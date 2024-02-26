using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Vote
{
    public interface IGroupService
    {
        #region crud
        Task<ModelResult<GroupDTO>> ReadSingleGroup(Guid id);
        Task<ModelResult<GroupDTO>> ReadAllGroups();
        Task<ModelResult<string>> CreateNewGroup(GroupDTO groupDTO);
        Task<string> UpdateGroupInformation(GroupDTO groupDTO);
        Task<string> DeleteGroup(Guid id);
        #endregion
    }
}
