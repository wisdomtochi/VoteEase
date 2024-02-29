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

        public VoteService(IGenericRepository<Vote> voteGenericRepository)
        {
            this.voteGenericRepository = voteGenericRepository;
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
            var checkVote = await voteGenericRepository.ReadSingle(vote.Id);
            if (checkVote != null) return Map.GetModelResult<string>(null, null, false, "Vote Already Exists");

            Vote newVote = new()
            {
                Id = vote.Id,
                Member = vote.Member,
                Counsellor = vote.Counsellor,
                PeoplesWarden = vote.PeoplesWarden
            };

            await voteGenericRepository.Create(newVote);
            await voteGenericRepository.SaveChanges();
            return Map.GetModelResult<string>(null, null, true, "Vote Added");
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
                    PeoplesWarden = vote.PeoplesWarden
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
