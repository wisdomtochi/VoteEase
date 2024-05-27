using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class GroupService : IGroupService
    {
        private readonly IGenericRepository<Group> groupGenericRepository;
        private readonly IGenericRepository<MemberInGroup> memberInGroupGenericRepository;

        public GroupService(IGenericRepository<Group> groupGenericRepository,
                            IGenericRepository<MemberInGroup> memberInGroupGenericRepository)
        {
            this.groupGenericRepository = groupGenericRepository;
            this.memberInGroupGenericRepository = memberInGroupGenericRepository;
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
                return Map.GetModelResult(thisGroup, null, true, string.Empty);
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
                if (thisGroup != null) return Map.GetModelResult<string>(null, null, false, "Group Already Exists.");

                Group newGroup = new()
                {
                    Id = Guid.NewGuid(),
                    Name = group.Name,
                    LeaderId = group.Leader.Id,
                    Leader = group.Leader,
                    DateAdded = DateTime.UtcNow
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

        public async Task<ModelResult<string>> UpdateGroupDetails(Group model, Guid groupId)
        {
            try
            {
                Group group = await groupGenericRepository.ReadSingle(groupId);
                if (group == null) return Map.GetModelResult<string>(null, null, false, "Group Not Found");

                group.Name = model.Name;
                group.LeaderId = model.LeaderId;
                group.Leader = model.Leader;
                group.DateAdded = DateTime.UtcNow;

                var isLeaderAlreadyInGroup = await memberInGroupGenericRepository.ReadSingle(group.LeaderId, group.Id);
                if (isLeaderAlreadyInGroup == null) return Map.GetModelResult<string>(null, null, false, "Add Leader To Group First.");

                groupGenericRepository.Update(group);
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

                var member = await memberInGroupGenericRepository.ReadSingle(group.LeaderId, group.Id);

                if (member != null)
                {
                    await memberInGroupGenericRepository.Delete(group.LeaderId, group.Id);
                    await memberInGroupGenericRepository.SaveChanges();
                }

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
