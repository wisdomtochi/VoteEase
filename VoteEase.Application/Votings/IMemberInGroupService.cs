using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Votings
{
    public interface IMemberInGroupService
    {
        Task<ModelResult<MemberInGroupDTOw>> GetMemberInGroup(Guid memberId, Guid groupId);
        Task<ModelResult<MemberInGroupDTOw>> GetAllMembersAndTheirGroups();
        Task<ModelResult<string>> AddNewMemberToGroup(MemberInGroup memberInGroup);
        Task<ModelResult<string>> UpdateMemberInGroup(Guid memberId, Guid groupId, MemberInGroup memberInGroup);
        Task<ModelResult<string>> DeleteMemberInGroup(Guid memberId, Guid groupId);
    }
}
