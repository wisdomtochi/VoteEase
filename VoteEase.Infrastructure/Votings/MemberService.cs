using VoteEase.Application.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> memberGenericRepository;
        private readonly IGenericRepository<MemberInGroup> memberInGroupGenericRepository;

        public MemberService(IGenericRepository<Member> memberGenericRepository,
                            IGenericRepository<MemberInGroup> memberInGroupGenericRepository)
        {
            this.memberGenericRepository = memberGenericRepository;
            this.memberInGroupGenericRepository = memberInGroupGenericRepository;
        }

        public async Task<ModelResult<string>> AddMembersFromExcel(List<MemberExcelSheet> model)
        {
            if (model == null || model.Count is 0) return Map.GetModelResult<string>(null, null, false, "Model cannot be empty.");

            var members = await memberGenericRepository.ReadAll();
            //List<Member> members = new();

            foreach (var item in model)
            {
                Group memberGroup = (Group)members.Where(x => x.Group.Name == item.GroupName);

                var member = new Member()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    DateCreated = DateTime.UtcNow,
                    Group = memberGroup ?? null
                };

                await memberGenericRepository.Create(member);
            }

            await memberGenericRepository.SaveChanges();
            return Map.GetModelResult<string>(null, null, true, "Members added successfully.");
        }

        #region crud
        public async Task<ModelResult<MemberDTO>> GetAllMembers()
        {
            try
            {
                var members = await memberGenericRepository.ReadAll();
                if (!members.Any()) return Map.GetModelResult<MemberDTO>(null, null, false, "No Member Found");

                List<MemberDTO> member = Map.Member(members);
                return Map.GetModelResult(null, member, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<MemberDTO>> GetMember(Guid id)
        {
            try
            {
                var member = await memberGenericRepository.ReadSingle(id);
                if (member == null) return Map.GetModelResult<MemberDTO>(null, null, false, "Member Not Found");

                var thisMember = Map.Member(member);
                return Map.GetModelResult(thisMember, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> CreateNewMember(Member member)
        {
            try
            {
                Member ifMemberIsNull = await memberGenericRepository.ReadSingle(member.Id);
                if (ifMemberIsNull != null) return Map.GetModelResult<string>(null, null, false, "Member Already Exists");

                Member newMember = new()
                {
                    Id = Guid.NewGuid(),
                    Name = member.Name,
                    PhoneNumber = member.PhoneNumber,
                    DateCreated = DateTime.UtcNow,
                    Group = member.Group
                };

                await memberGenericRepository.Create(newMember);
                await memberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Added");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> UpdateMember(Member model, Guid memberId)
        {
            try
            {
                Member member = await memberGenericRepository.ReadSingle(memberId);
                if (member == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found");

                member.Name = model.Name;
                member.PhoneNumber = model.PhoneNumber;
                member.DateCreated = DateTime.UtcNow;
                member.Group = model.Group;

                if (member.Group != null)
                {
                    MemberInGroup memberInGroupExists = await memberInGroupGenericRepository.ReadSingle(memberId, member.Group.Id);

                    memberInGroupExists.MemberId = memberId;
                    memberInGroupExists.Member = member;
                    memberInGroupExists.GroupId = member.Group.Id;
                    memberInGroupExists.Group = member.Group;

                    if (memberInGroupExists != null) memberInGroupGenericRepository.Update(memberInGroupExists);
                    await memberInGroupGenericRepository.SaveChanges();
                }

                memberGenericRepository.Update(member);
                await memberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Details Updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> DeleteMember(Guid memberId)
        {
            try
            {
                Member memberExists = await memberGenericRepository.ReadSingle(memberId);
                if (memberExists == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found");

                if (memberExists.Group != null)
                {
                    var memberInGroupExists = await memberInGroupGenericRepository.ReadSingle(memberId, memberExists.Group.Id);

                    if (memberInGroupExists != null) await memberInGroupGenericRepository.Delete(memberId, memberExists.Group.Id);
                    await memberInGroupGenericRepository.SaveChanges();
                }

                await memberGenericRepository.Delete(memberId);
                await memberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Deleted");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
