using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> memberGenericRepository;

        public MemberService(IGenericRepository<Member> memberGenericRepository)
        {
            this.memberGenericRepository = memberGenericRepository;
        }

        /// <summary>
        /// Method to get all accredited members
        /// </summary>
        /// <returns></returns>
        public async Task<ModelResult<MemberDTO>> GetAllAccreditedMembers()
        {
            try
            {
                var members = await memberGenericRepository.ReadAll();

                var membersList = Map.Member(members).Where(x => x.IsAccredited.Equals(true)).ToList();

                return Map.GetModelResult(null, membersList, true, "All Accredited Members");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                    Id = member.Id,
                    Name = member.Name,
                    PhoneNumber = member.PhoneNumber,
                    IsAccredited = member.IsAccredited
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
                Member isMemberNull = await memberGenericRepository.ReadSingle(memberId);
                if (isMemberNull == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found");

                Member newMember = new()
                {
                    Id = memberId,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    IsAccredited = model.IsAccredited
                };

                memberGenericRepository.Update(newMember);
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
                Member isMemberNull = await memberGenericRepository.ReadSingle(memberId);
                if (isMemberNull == null) return Map.GetModelResult<string>(null, null, false, "Member Not Found");

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
