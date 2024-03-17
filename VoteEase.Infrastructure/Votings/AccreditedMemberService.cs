using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class AccreditedMemberService : IAccreditedMemberService
    {
        private readonly IGenericRepository<AccreditedMember> accreditedMemberGenericRepository;
        private readonly IGenericRepository<Member> memberGenericRepository;

        public AccreditedMemberService(IGenericRepository<AccreditedMember> accreditedMemberGenericRepository,
                                        IGenericRepository<Member> memberGenericRepository)
        {
            this.accreditedMemberGenericRepository = accreditedMemberGenericRepository;
            this.memberGenericRepository = memberGenericRepository;
        }

        public async Task<ModelResult<string>> CreateListOfAccreditedMember(List<AccreditedMember> members)
        {
            try
            {
                if (members is null) return Map.GetModelResult<string>(null, null, false, "List of members cannot be empty.");
                var checkMembers = await memberGenericRepository.ReadAll();

                foreach (var member in members)
                {
                    var newMembers = checkMembers.Where(m => m.Equals(member));

                    accreditedMemberGenericRepository.AddRange((IEnumerable<AccreditedMember>)newMembers);
                    await accreditedMemberGenericRepository.SaveChanges();
                    return Map.GetModelResult<string>(null, null, true, "Members Added Successfully.");
                }

                return Map.GetModelResult<string>(null, null, true, "Members Do Not Exist.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> RemoveListOfAccreditedMember(List<AccreditedMember> members)
        {
            try
            {
                if (members is null) return Map.GetModelResult<string>(null, null, false, "List of members cannot be empty.");

                var checkMembers = await memberGenericRepository.ReadAll();

                foreach (var member in members)
                {
                    var newMembers = checkMembers.Where(m => m.Equals(member));

                    accreditedMemberGenericRepository.RemoveRange((IEnumerable<AccreditedMember>)newMembers);
                    await accreditedMemberGenericRepository.SaveChanges();
                    return Map.GetModelResult<string>(null, null, true, "Members Deleted Successfully.");
                }

                return Map.GetModelResult<string>(null, null, true, "Members Do Not Exist.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region CRUD
        public async Task<ModelResult<AccreditedMemberDTO>> GetAccreditedMembers()
        {
            try
            {
                var members = await accreditedMemberGenericRepository.ReadAll();
                if (!members.Any()) return Map.GetModelResult<AccreditedMemberDTO>(null, null, false, "No Accredited Member Found.");

                var membersList = Map.AccreditedMember(members);
                return Map.GetModelResult(null, membersList, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModelResult<AccreditedMemberDTO>> GetAccreditedMember(Guid memberId)
        {
            try
            {
                var member = await accreditedMemberGenericRepository.ReadSingle(memberId);
                if (member == null) return Map.GetModelResult<AccreditedMemberDTO>(null, null, false, "Accredited Member Found.");

                var thisMember = Map.AccreditedMember(member);
                return Map.GetModelResult(thisMember, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> CreateNewAccreditedMember(AccreditedMember member)
        {
            try
            {
                var checkIfMemberIsInMembersTable = await memberGenericRepository.ReadSingle(member.MemberId);

                if (checkIfMemberIsInMembersTable == null) return Map.GetModelResult<string>(null, null, false, "Member Does Not Exist.");

                AccreditedMember accreditedMember = new()
                {
                    MemberId = member.MemberId,
                    DateAdded = DateTime.UtcNow
                };

                await accreditedMemberGenericRepository.Create(accreditedMember);
                await accreditedMemberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member is now accredited.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ModelResult<string>> UpdateAccreditedMember(AccreditedMember member, Guid memberId)
        {
            try
            {
                var checkIfMemberIsInMembersTable = await memberGenericRepository.ReadSingle(memberId);

                if (checkIfMemberIsInMembersTable == null) return Map.GetModelResult<string>(null, null, false, "Member Does Not Exist.");

                AccreditedMember accreditedMember = new()
                {
                    MemberId = member.MemberId,
                    DateAdded = DateTime.UtcNow
                };

                accreditedMemberGenericRepository.Update(accreditedMember);
                await accreditedMemberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Member Details Updated Successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModelResult<string>> DeleteAccreditedMember(Guid memberId)
        {
            try
            {
                var member = await accreditedMemberGenericRepository.ReadSingle(memberId);

                if (member == null) return Map.GetModelResult<string>(null, null, false, "Member Does Not Exist.");

                await accreditedMemberGenericRepository.Delete(memberId);
                await accreditedMemberGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Members Deleted Successfully.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
