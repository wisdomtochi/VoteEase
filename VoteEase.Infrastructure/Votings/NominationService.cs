using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Core;
using VoteEase.Domain.Enums;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class NominationService : INominationService
    {
        private readonly IGenericRepository<Nomination> nominationGenericRepository;
        private readonly IGenericRepository<Member> memberGenericRepository;

        public NominationService(IGenericRepository<Nomination> nominationGenericRepository,
                                IGenericRepository<Member> memberGenericRepository)
        {
            this.nominationGenericRepository = nominationGenericRepository;
            this.memberGenericRepository = memberGenericRepository;
        }

        #region crud
        public async Task<ModelResult<NominationDTOw>> GetAllNominations()
        {
            try
            {
                var nominations = await nominationGenericRepository.ReadAll();
                if (!nominations.Any()) return Map.GetModelResult<NominationDTOw>(null, null, false, "No Nomination Found");

                var nominationList = Map.Nomination(nominations);
                return Map.GetModelResult(null, nominationList, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<NominationDTOw>> GetNomination(Guid nominationId)
        {
            try
            {
                Nomination nomination = await nominationGenericRepository.ReadSingle(nominationId);
                if (nomination == null) return Map.GetModelResult<NominationDTOw>(null, null, false, "Nomination Not Found");

                var thisNomination = Map.Nomination(nomination);
                return Map.GetModelResult(thisNomination, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> CreateNewNomination(Nomination nomination)
        {
            try
            {
                var nominationList = await nominationGenericRepository.ReadAll();

                Nomination newNomination = new()
                {
                    Id = Guid.NewGuid(),
                    GroupId = nomination.GroupId,
                    Group = nomination.Group,
                    MemberId = nomination.MemberId,
                    Member = nomination.Member,
                    Category = nomination.Category,
                    DateCreated = DateTime.UtcNow,
                };

                var member = await memberGenericRepository.ReadSingle(newNomination.MemberId);
                if (member == null) return Map.GetModelResult<string>(null, null, false, "Member cannot be found.");

                if (!newNomination.GroupId.Equals(member.Group.Id)) return Map.GetModelResult<string>(null, null, false, "Nominate members only from your group.");

                await nominationGenericRepository.Create(newNomination);
                await nominationGenericRepository.SaveChanges();

                var checkTheNumberOfTimesAGroupNominatedACounsellor = nominationList.Count(n => n.GroupId == newNomination.GroupId && n.Category == Category.Counsellor);
                if (checkTheNumberOfTimesAGroupNominatedACounsellor < 3) return Map.GetModelResult<string>(null, null, false, "Nominate Only Three Counsellors.");

                return Map.GetModelResult<string>(null, null, true, "Nomination Successful.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ModelResult<string>> UpdateNomination(Nomination nomination, Guid nominationId)
        {
            try
            {
                Nomination checkNomination = await nominationGenericRepository.ReadSingle(nominationId);
                if (checkNomination == null) return Map.GetModelResult<string>(null, null, false, "Nomination Not Found");

                checkNomination.GroupId = nomination.GroupId;
                checkNomination.Group = nomination.Group;
                checkNomination.MemberId = nomination.MemberId;
                checkNomination.Member = nomination.Member;
                checkNomination.DateCreated = DateTime.UtcNow;
                checkNomination.Category = nomination.Category;

                nominationGenericRepository.Update(checkNomination);
                await nominationGenericRepository.SaveChanges();

                return Map.GetModelResult<string>(null, null, true, "Nomination Has Been Updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> DeleteNomination(Guid nominationId)
        {
            try
            {
                var checkNomination = await nominationGenericRepository.ReadSingle(nominationId);
                if (checkNomination == null) return Map.GetModelResult<string>(null, null, false, "Nomination Not Found");

                await nominationGenericRepository.Delete(nominationId);
                await nominationGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Nomination Deleted");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
