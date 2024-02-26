using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Vote
{
    public interface IMemberService
    {
        Task<ModelResult<NominationDTO>> GetAllNominatedPersons();

        #region crud
        Task<ModelResult<MemberDTO>> ReadSingleMember(Guid id);
        Task<ModelResult<MemberDTO>> ReadAllMembers();
        Task<ModelResult<string>> CreateNewMember(MemberDTO memberDTO);
        Task<string> UpdateMember(MemberDTO memberDTO);
        Task<string> DeleteMember(Guid id);
        #endregion
    }
}
