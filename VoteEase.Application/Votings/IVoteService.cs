using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface IVoteService
    {
        Task<ModelResult<VoteResultDTO>> CountVoteResult();
        #region crud
        Task<ModelResult<VoteDTO>> GetVote(Guid id);
        Task<ModelResult<VoteDTO>> GetAllVotes();
        Task<ModelResult<string>> CreateNewVote(Vote vote);
        Task<ModelResult<string>> UpdateVote(Vote vote, Guid voteId);
        Task<ModelResult<string>> DeleteVote(Guid voteId);
        #endregion
    }
}
