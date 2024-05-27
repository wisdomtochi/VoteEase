using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.Domain.Enums;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class VoteService : IVoteService
    {
        private readonly IGenericRepository<Vote> voteGenericRepository;
        private readonly IGenericRepository<Nomination> nominationGenericRepository;
        private readonly IGenericRepository<AccreditedMember> accreditedMemberGenericRepository;

        public VoteService(IGenericRepository<Vote> voteGenericRepository,
                            IGenericRepository<Nomination> nominationGenericRepository,
                                  IGenericRepository<AccreditedMember> accreditedMemberGenericRepository)
        {
            this.voteGenericRepository = voteGenericRepository;
            this.nominationGenericRepository = nominationGenericRepository;
            this.accreditedMemberGenericRepository = accreditedMemberGenericRepository;
        }

        public async Task<ModelResult<VoteResultDTO>> CountVoteResult()
        {
            try
            {
                IEnumerable<Vote> allVotes = await voteGenericRepository.ReadAll();
                //if (!allVotes.Any()) return Map.GetModelResult<string>(null, null, false, "No vote found");

                var counsellorVotes = allVotes
                                .GroupBy(vote => vote.Category == Category.Counsellor)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.MemberId)
                                                    .Select(x => new
                                                    {
                                                        MemberName = x.Key,
                                                        Count = x.Count()
                                                    })
                                });

                var peoplesWardenVotes = allVotes
                                .GroupBy(vote => vote.Category == Category.PeopleWarden)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.MemberId)
                                                    .Select(x => new
                                                    {
                                                        MemberName = x.Key,
                                                        Count = x.Count()
                                                    })
                                });

                var synodDelegateVotes = allVotes
                                .GroupBy(vote => vote.Category == Category.SynodDelegate)
                                .Select(group => new
                                {
                                    Category = group.Key,
                                    Members = group.GroupBy(vote => vote.MemberId)
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
                var checkVote = await voteGenericRepository.ReadSingle(vote.Id);
                if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Already Exists");

                Vote newVote = new()
                {
                    Id = Guid.NewGuid(),
                    NominationId = vote.NominationId,
                    VotedPerson = vote.VotedPerson,
                    MemberId = vote.MemberId,
                    Voter = vote.Voter,
                    Category = vote.Category,
                    DateCreated = vote.DateCreated
                };

                var wasVotedPersonNominated = await nominationGenericRepository.ReadSingle(newVote.NominationId);
                if (wasVotedPersonNominated != null) return Map.GetModelResult<string>(null, null, false, "The Voted Person Was Not Nominated.");

                //checking if the member is accredited
                var memberIsAccredited = await accreditedMemberGenericRepository.ReadSingle(newVote.MemberId);
                if (memberIsAccredited == null) return Map.GetModelResult<string>(null, null, false, "Vote Unsuccessful. You are unaccredited.");

                await voteGenericRepository.Create(newVote);
                await voteGenericRepository.SaveChanges();

                return Map.GetModelResult<string>(null, null, true, "Vote Successful.");
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
                Vote checkVote = await voteGenericRepository.ReadSingle(voteId);
                if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Not Found");

                checkVote.NominationId = vote.NominationId;
                checkVote.VotedPerson = vote.VotedPerson;
                checkVote.MemberId = vote.MemberId;
                checkVote.Voter = vote.Voter;
                checkVote.Category = vote.Category;
                checkVote.DateCreated = vote.DateCreated;

                var wasVotedPersonNominated = await nominationGenericRepository.ReadSingle(checkVote.NominationId);
                if (wasVotedPersonNominated != null) return Map.GetModelResult<string>(null, null, false, "The Voted Person Was Not Nominated.");

                //checking if the member is accredited
                var memberIsAccredited = await accreditedMemberGenericRepository.ReadSingle(checkVote.MemberId);
                if (memberIsAccredited == null) return Map.GetModelResult<string>(null, null, false, "Vote Unsuccessful. You are unaccredited.");

                voteGenericRepository.Update(checkVote);
                await voteGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, false, "Vote Successful.");
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
