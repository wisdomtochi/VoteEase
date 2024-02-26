using VoteEase.Application.Vote;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Infrastructure.Vote
{
    public class MemberService : IMemberService
    {
        private readonly Data_Access.Interface.IGenericRepository genericRepository;

        public MemberService(IGenericRepository genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public Task<ModelResult<string>> CreateNewMember(MemberDTO memberDTO)
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

        public Task<ModelResult<MemberDTO>> ReadAllMembers()
        {
            throw new NotImplementedException();
        }

        public Task<ModelResult<MemberDTO>> ReadSingleMember(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateMember(MemberDTO memberDTO)
        {
            throw new NotImplementedException();
        }
    }
}
