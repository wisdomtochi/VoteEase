using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class VoteService : IVoteService
    {
        private readonly IGenericRepository<Vote> voteGenericRepository;
        private readonly IGenericRepository<Nomination> nominationGenericRepository;
        private readonly IGenericRepository<Member> memberGenericRepository;

        public VoteService(IGenericRepository<Vote> voteGenericRepository,
                            IGenericRepository<Nomination> nominationGenericRepository,
                                  IGenericRepository<Member> memberGenericRepository)
        {
            this.voteGenericRepository = voteGenericRepository;
            this.nominationGenericRepository = nominationGenericRepository;
            this.memberGenericRepository = memberGenericRepository;
        }

        public async Task<ModelResult<VoteResultDTO>> CountVoteResult()
        {
            try
            {
                IEnumerable<Vote> allVotes = await voteGenericRepository.ReadAll();
                //if (!allVotes.Any()) return Map.GetModelResult<string>(null, null, false, "No vote found");

                var counsellorVotes = allVotes
                                .Where(vote => vote.Counsellor != null)
                                .GroupBy(vote => vote.Counsellor)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.Counsellor.Name)
                                                    .Select(x => new
                                                    {
                                                        MemberName = x.Key,
                                                        Count = x.Count()
                                                    })
                                });

                var peoplesWardenVotes = allVotes
                                .Where(vote => vote.PeoplesWarden != null)
                                .GroupBy(vote => vote.PeoplesWarden)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.PeoplesWarden.Name)
                                                    .Select(x => new
                                                    {
                                                        MemberName = x.Key,
                                                        Count = x.Count()
                                                    })
                                });

                var synodDelegateVotes = allVotes
                                .Where(vote => vote.SynodDelegate != null)
                                .GroupBy(vote => vote.SynodDelegate)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.SynodDelegate.Name)
                                                    .Select(x => new
                                                    {
                                                        MemberName = x.Key,
                                                        Count = x.Count()
                                                    })
                                });

                var voteResult = new VoteResultDTO
                {
                    CounsellorVotes = (Dictionary<string, IEnumerable<MemberCount>>)counsellorVotes,
                    PeoplesWardenVotes = (Dictionary<string, IEnumerable<MemberCount>>)peoplesWardenVotes,
                    SynodDelegateVotes = (Dictionary<string, IEnumerable<MemberCount>>)synodDelegateVotes
                };

                var result = Map.VoteResult(voteResult);
                return Map.GetModelResult(result, null, true, "Vote Result");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region crud
        public async Task<ModelResult<VoteDTO>> GetVote(Guid id)
        {
            try
            {
                var vote = await voteGenericRepository.ReadSingle(id);
                if (vote == null) return Map.GetModelResult<VoteDTO>(null, null, false, "Vote Not Found");

                var thisVote = Map.Vote(vote);
                return Map.GetModelResult(thisVote, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<VoteDTO>> GetAllVotes()
        {
            try
            {
                var votes = await voteGenericRepository.ReadAll();
                if (!votes.Any()) return Map.GetModelResult<VoteDTO>(null, null, false, "No Vote Found");

                var thisVote = Map.Vote(votes);
                return Map.GetModelResult(null, thisVote, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> CreateNewVote(Vote vote)
        {
            try
            {
                var nominations = await nominationGenericRepository.ReadAll();
                var checkVote = await voteGenericRepository.ReadSingle(vote.Id);
                if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Already Exists");



                Vote newVote = new()
                {
                    Id = vote.Id,
                    Member = vote.Member,
                    Counsellor = vote.Counsellor,
                    PeoplesWarden = vote.PeoplesWarden,
                    SynodDelegate = vote.SynodDelegate
                };

                //checking if the member is accredited
                var memberIsAccredited = await memberGenericRepository.ReadSingle(newVote.Member.Id);
                if (!memberIsAccredited.IsAccredited.Equals(true)) return Map.GetModelResult<string>(null, null, false, "Vote Unsuccessful. " + memberIsAccredited.Name + " you cannot vote because you are unaccredited.");

                //iterating through the nominations in the database
                foreach (var nomination in nominations)
                {
                    //checking if the new vote counsellor matches any of the counsellor in the the current iterated nomination entity
                    if (newVote.Counsellor == nomination.Counsellors.CounsellorOne || newVote.Counsellor == nomination.Counsellors.CounsellorTwo
                                || newVote.Counsellor == nomination.Counsellors.CounsellorThree)
                    {
                        // Checking if the people's warden section of the current iterated nomination entity and the new vote are not null
                        if (nomination.PeoplesWarden.PeoplesWarden != null && newVote.PeoplesWarden != null)
                        {
                            //checking if the new vote people's warden matches the people's warden in the current iterated nomination entity
                            //before creating the new vote to the database
                            if (newVote.PeoplesWarden == nomination.PeoplesWarden.PeoplesWarden)
                            {
                                // Checking if the synod delegates section of the current iterated nomination entity and the new vote are not null
                                if (nomination.Delegates.Delegate != null && newVote.SynodDelegate != null)
                                {
                                    //checking if the new vote synod delegate matches the synod delegate in the current iterated nomination entity
                                    //before creating the new vote to the database
                                    if (newVote.SynodDelegate == nomination.Delegates.Delegate)
                                    {
                                        await voteGenericRepository.Create(newVote);
                                        await voteGenericRepository.SaveChanges();
                                        return Map.GetModelResult<string>(null, null, true, "Vote Added");
                                    }
                                    // if they don't match, I am returning an unsuccessful vote with a detailed explanation
                                    else
                                    {
                                        return Map.GetModelResult<string>(null, null, false, "Vote Unseccessful. The " + newVote.SynodDelegate + "was not nominated");
                                    }
                                }
                                //checking if the new vote synod delegate is null. If null, then the app is creating the vote with the synod delegate field to be empty,
                                //because the user did not enter anything in it.
                                else if (newVote.SynodDelegate == null)
                                {
                                    await voteGenericRepository.Create(newVote);
                                    await voteGenericRepository.SaveChanges();
                                    return Map.GetModelResult<string>(null, null, true, "Vote Added");
                                }
                            }
                            //if they don't match, I am returning an unsuccessful vote with a detailed explanation
                            else
                            {
                                return Map.GetModelResult<string>(null, null, false, "Vote Unseccessful. The " + newVote.PeoplesWarden + "was not nominated");
                            }
                        }
                        //checking if the people's warden field of the new vote is empty, so the app can create the new vote
                        else if (newVote.PeoplesWarden == null)
                        {
                            // Checking if the synod delegates section of the current iterated nomination entity and the new vote are not null
                            if (nomination.Delegates.Delegate != null && newVote.SynodDelegate != null)
                            {
                                //checking if the new vote synod delegate matches the synod delegate in the current iterated nomination entity
                                //before creating the new vote to the database
                                if (newVote.SynodDelegate == nomination.Delegates.Delegate)
                                {
                                    await voteGenericRepository.Create(newVote);
                                    await voteGenericRepository.SaveChanges();
                                    return Map.GetModelResult<string>(null, null, true, "Vote Added");
                                }
                                // if they don't match, I am returning an unsuccessful vote with a detailed explanation
                                else
                                {
                                    return Map.GetModelResult<string>(null, null, false, "Vote Unseccessful. The " + newVote.SynodDelegate + "was not nominated");
                                }
                            }
                            //checking if the new vote synod delegate is null. If null, then the app is creating the vote with the synod delegate field to be empty,
                            //because the user did not enter anything in it.
                            else if (newVote.SynodDelegate == null)
                            {
                                await voteGenericRepository.Create(newVote);
                                await voteGenericRepository.SaveChanges();
                                return Map.GetModelResult<string>(null, null, true, "Vote Added");
                            }
                        }
                    }
                }
                return Map.GetModelResult<string>(null, null, false, "Vote Unseccessful. The " + newVote.Counsellor + "was not nominated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> UpdateVote(Vote vote, Guid voteId)
        {
            try
            {
                var checkVote = await voteGenericRepository.ReadSingle(voteId);
                if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Not Found");

                Vote newVote = new()
                {
                    Id = vote.Id,
                    Member = vote.Member,
                    Counsellor = vote.Counsellor,
                    PeoplesWarden = vote.PeoplesWarden,
                    SynodDelegate = vote.SynodDelegate
                };

                voteGenericRepository.Update(newVote);
                await voteGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Vote Updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> DeleteVote(Guid voteId)
        {
            try
            {
                var checkVote = await voteGenericRepository.ReadSingle(voteId);
                if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Not Found");

                await voteGenericRepository.Delete(voteId);
                await voteGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Vote Deleted");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
