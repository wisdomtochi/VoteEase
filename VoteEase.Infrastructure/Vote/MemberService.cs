using VoteEase.Application.Vote;
using VoteEase.Data_Access.Interface;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;
//using Map = VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Vote
{
    public class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> memberGenericRepository;

        public MemberService(IGenericRepository<Member> memberGenericRepository)
        {
            this.memberGenericRepository = memberGenericRepository;
        }

        public async Task<ModelResult<MemberDTO>> GetAllMembers()
        {
            try
            {
                var members = await memberGenericRepository.ReadAll();

                List<MemberDTO> member = Map.Member(members);
                return Map.GetModelResult(member, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberDTO> GetMember(Guid id)
        {
            try
            {
                var member = await memberGenericRepository.ReadSingle(id);
                return Map.Member(member);
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
                if (ifMemberIsNull == null)
                {
                    Member newMember = new()
                    {
                        Id = member.Id,
                        Name = member.Name,
                        PhoneNumber = member.PhoneNumber,
                        IsAccredited = member.IsAccredited
                    };

                    await memberGenericRepository.Create(newMember);
                    await memberGenericRepository.SaveChanges();
                    return Map.GetModelResult<string>(null, true, "Member Added");
                }
                else
                {
                    return Map.GetModelResult<string>(null, false, "Member Not Added");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> UpdateMember(MemberDTO memberDTO)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteMember(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ModelResult<NominationDTO>> GetAllNominatedPersons()
        {
            throw new NotImplementedException();
        }
    }
}
