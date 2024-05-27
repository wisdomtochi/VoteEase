using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class MemberInGroupService : IMemberInGroupService
    {
        private readonly IGenericRepository<MemberInGroup> memberInGroupGenericRepository;
        private readonly IGenericRepository<Member> memberGenericRepository;
        private readonly IGenericRepository<Group> groupGenericRepository;

        public MemberInGroupService(IGenericRepository<MemberInGroup> memberInGroupGenericRepository,
                                    IGenericRepository<Member> memberGenericRepository,
                                    IGenericRepository<Group> groupGenericRepository)
        {
            this.memberInGroupGenericRepository = memberInGroupGenericRepository;
            this.memberGenericRepository = memberGenericRepository;
            this.groupGenericRepository = groupGenericRepository;
        }

        public async Task<ModelResult<MemberInGroupDTOw>> GetAllMembersAndTheirGroups()
        {
            try
            {
                var members = await memberInGroupGenericRepository.ReadAll();
                if (!members.Any()) return Map.GetModelResult<MemberInGroupDTOw>(null, null, false, "No Member Is In A Group.");

                var membersInGroups = Map.MemberInGroup(members);

                return Map.GetModelResult(null, membersInGroups, true, string.Empty);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModelResult<MemberInGroupDTOw>> GetMemberInGroup(Guid memberId, Guid groupId)
        {
            try
            {
                var member = await memberInGroupGenericRepository.ReadSingle(memberId, groupId);

                if (member == null) return Map.GetModelResult<MemberInGroupDTOw>(null, null, false, "Member Not Found.");

                var memberInGroup = Map.MemberInGroup(member);

                return Map.GetModelResult(memberInGroup, null, true, string.Empty);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> AddNewMemberToGroup(MemberInGroup memberInGroup)
        {
            try
            {
                var isMemberInGroup = await memberInGroupGenericRepository.ReadSingle(memberInGroup.MemberId, memberInGroup.GroupId);

                if (isMemberInGroup != null) return Map.GetModelResult<string>(null, null, false, "Member Already Exists.");

                var memberExists = await memberGenericRepository.ReadSingle(memberInGroup.MemberId);

                if (memberExists == null) Map.GetModelResult<string>(null, null, false, "Member Not Found.");

                var groupExists = await groupGenericRepository.ReadSingle(memberInGroup.GroupId);

                if (groupExists == null) return Map.GetModelResult<string>(null, null, false, "Group Not Found.");

                MemberInGroup addNewMemberToGroup = new()
                {
                    MemberId = memberInGroup.MemberId,
                    Member = memberInGroup.Member,
                    GroupId = memberInGroup.GroupId,
                    Group = memberInGroup.Group
                };

                Member updateMember = new()
                {
                    Id = memberInGroup.MemberId,
                    Name = memberInGroup.Member.Name,
                    PhoneNumber = memberInGroup.Member.PhoneNumber,
                    DateCreated = memberInGroup.Member.DateCreated,
                    Group = memberInGroup.Group
                };

                memberGenericRepository.Update(updateMember);
                await memberGenericRepository.SaveChanges();

                await memberInGroupGenericRepository.Create(addNewMemberToGroup);
                await memberInGroupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Has Been Added Successfully.");
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> UpdateMemberInGroup(Guid memberId, Guid groupId, MemberInGroup memberInGroup)
        {
            try
            {
                var memberExists = await memberInGroupGenericRepository.ReadSingle(memberId, groupId);

                if (memberExists == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found.");

                MemberInGroup updateMemberInGroup = new()
                {
                    MemberId = memberId,
                    Member = memberInGroup.Member,
                    GroupId = groupId,
                    Group = memberInGroup.Group
                };

                Member updateMember = new()
                {
                    Id = memberId,
                    Name = memberInGroup.Member.Name,
                    PhoneNumber = memberInGroup.Member.PhoneNumber,
                    DateCreated = memberInGroup.Member.DateCreated,
                    Group = memberInGroup.Group
                };

                memberGenericRepository.Update(updateMember);
                await memberGenericRepository.SaveChanges();

                memberInGroupGenericRepository.Update(updateMemberInGroup);
                await memberInGroupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Updated Successfully.");
            }
            catch
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> DeleteMemberInGroup(Guid memberId, Guid groupId)
        {
            try
            {
                var memberExists = await memberInGroupGenericRepository.ReadSingle(memberId, groupId);

                if (memberExists == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found.");

                await memberInGroupGenericRepository.Delete(memberId, groupId);
                await memberInGroupGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Deleted Successfully.");
            }
            catch
            {
                throw;
            }
        }
    }
}
