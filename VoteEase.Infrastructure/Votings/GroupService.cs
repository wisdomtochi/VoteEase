using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class GroupService : IGroupService
    {
        private readonly IGenericRepository<Group> groupGenericRepository;

        public GroupService(IGenericRepository<Group> groupGenericRepository)
        {
            this.groupGenericRepository = groupGenericRepository;
        }

        #region crud
        public async Task<ModelResult<GroupDTO>> GetAllGroups()
        {
            try
            {
                var groups = await groupGenericRepository.ReadAll();
                if (!groups.Any()) return Map.GetModelResult<GroupDTO>(null, null, false, "No Group Found");

                List<GroupDTO> groupCollection = Map.Group(groups);
                return Map.GetModelResult(null, groupCollection, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<GroupDTO>> GetGroup(Guid groupId)
        {
            try
            {
                Group group = await groupGenericRepository.ReadSingle(groupId);
                if (group == null) return Map.GetModelResult<GroupDTO>(null, null, false, "Group Not Found");

                var thisGroup = Map.Group(group);
                return Map.GetModelResult<GroupDTO>(thisGroup, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> CreateNewGroup(Group group)
        {
            try
            {
                var thisGroup = await groupGenericRepository.ReadSingle(group.Id);
                if (thisGroup != null) return Map.GetModelResult<string>(null, null, false, "Group Already Exists");

                Group newGroup = new()
                {
                    Id = group.Id,
                    Name = group.Name,
                    LeaderId = group.LeaderId,
                    Leader = group.Leader
                };

                await groupGenericRepository.Create(newGroup);
                await groupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Group Created");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> UpdateGroupDetails(Group group, Guid groupId)
        {
            try
            {
                Group thisGroup = await groupGenericRepository.ReadSingle(groupId);
                if (thisGroup == null) return Map.GetModelResult<string>(null, null, false, "Group Not Found");

                Group updateGroup = new()
                {
                    Id = group.Id,
                    Name = group.Name,
                    LeaderId = group.LeaderId,
                    Leader = group.Leader
                };

                groupGenericRepository.Update(updateGroup);
                await groupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Group Details Updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> DeleteGroup(Guid groupId)
        {
            try
            {
                Group group = await groupGenericRepository.ReadSingle(groupId);
                if (group == null) return Map.GetModelResult<string>(null, null, false, "Group Not Found");

                await groupGenericRepository.Delete(groupId);
                await groupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Group Deleted");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
