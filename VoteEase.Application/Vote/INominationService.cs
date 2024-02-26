using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Vote
{
    public interface INominationService
    {
        #region crud
        Task<ModelResult<NominationDTOw>> ReadSingleNomination(Guid id);
        Task<ModelResult<NominationDTOw>> ReadAllNominations();
        Task<ModelResult<string>> CreateNewNomination(NominationDTOw nominationDTO);
        Task<string> UpdateNomination(NominationDTOw nominationDTO);
        Task<string> DeleteNomination(Guid id);
        #endregion
    }
}
