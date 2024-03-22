using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface INominationService
    {
        #region crud
        Task<ModelResult<NominationDTOw>> GetNomination(Guid nominationId);
        Task<ModelResult<NominationDTOw>> GetAllNominations();
        Task<ModelResult<string>> CreateNewNomination(Nomination nomination);
        Task<ModelResult<string>> UpdateNomination(Nomination nomination, Guid nominationId);
        Task<ModelResult<string>> DeleteNomination(Guid nominationId);
        #endregion
    }
}
