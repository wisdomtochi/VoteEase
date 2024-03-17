using VoteEase.Application.Helpers;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface IMemberService
    {
        Task<ModelResult<string>> AddMembersFromExcel(List<MemberExcelSheet> model);

        #region crud
        Task<ModelResult<MemberDTO>> GetMember(Guid id);
        Task<ModelResult<MemberDTO>> GetAllMembers();
        Task<ModelResult<string>> CreateNewMember(Member member);
        Task<ModelResult<string>> UpdateMember(Member model, Guid memberId);
        Task<ModelResult<string>> DeleteMember(Guid id);
        #endregion
    }
}
