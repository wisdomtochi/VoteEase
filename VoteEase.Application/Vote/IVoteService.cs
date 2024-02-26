using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Vote
{
    public interface IVoteService
    {
        #region crud
        Task<ModelResult<VoteDTO>> ReadSingleVote(Guid id);
        Task<ModelResult<VoteDTO>> ReadAllVotes();
        Task<ModelResult<string>> CreateNewVote(VoteDTO voteDTO);
        Task<string> UpdateVote(VoteDTO voteDTO);
        Task<string> DeleteVote(Guid id);
        #endregion
    }
}
