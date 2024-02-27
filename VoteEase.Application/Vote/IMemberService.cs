using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Vote
{
    public interface IMemberService
    {
        Task<ModelResult<NominationDTO>> GetAllNominatedPersons();

        #region crud
        Task<MemberDTO> GetMember(Guid id);
        Task<ModelResult<MemberDTO>> GetAllMembers();
        Task<ModelResult<string>> CreateNewMember(Member member);
        Task<string> UpdateMember(MemberDTO memberDTO);
        Task<string> DeleteMember(Guid id);
        #endregion
    }
}
