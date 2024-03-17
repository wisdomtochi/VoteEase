using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface IAccreditedMemberService
    {
        Task<ModelResult<string>> CreateListOfAccreditedMember(List<AccreditedMember> members);
        Task<ModelResult<string>> RemoveListOfAccreditedMember(List<AccreditedMember> members);

        #region CRUD
        Task<ModelResult<AccreditedMemberDTO>> GetAccreditedMember(Guid memberId);
        Task<ModelResult<AccreditedMemberDTO>> GetAccreditedMembers();
        Task<ModelResult<string>> CreateNewAccreditedMember(AccreditedMember member);
        Task<ModelResult<string>> UpdateAccreditedMember(AccreditedMember member, Guid memberId);
        Task<ModelResult<string>> DeleteAccreditedMember(Guid memberId);
        #endregion
    }
}
