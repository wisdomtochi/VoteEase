using VoteEase.Application.Votings;
using VoteEase.Data_Access.Interface;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Votings
{
    public class NominationService : INominationService
    {
        private readonly IGenericRepository<Nomination> nominationGenericRepository;

        public NominationService(IGenericRepository<Nomination> nominationGenericRepository)
        {
            this.nominationGenericRepository = nominationGenericRepository;
        }

        public async Task<ModelResult<NominationDTO>> GetNominatedPersons()
        {
            try
            {
                var nominations = await nominationGenericRepository.ReadAll();
                if (!nominations.Any()) return Map.GetModelResult<NominationDTO>(null, null, false, "No Nomination Found");

                List<NominationDTO> nominationList = Map.Nomination(nominations);

                return Map.GetModelResult(null, nominationList, true, "List of Nominated Persons");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<NominationWithoutDelegatesDTO>> GetNominationsWithoutDelegates()
        {
            try
            {
                var nominations = await nominationGenericRepository.ReadAll();
                if (!nominations.Any()) return Map.GetModelResult<NominationWithoutDelegatesDTO>(null, null, false, "No Nomination Found");

                List<NominationWithoutDelegatesDTO> nominationList = Map.PeoplesNominationWithoutDelegates(nominations);
                return Map.GetModelResult(null, nominationList, true, "List of Nominated Persons");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<NominationWithoutDelegatesDTO>> GetSpecificNominationWithoutDelegate(Guid nominationId)
        {
            try
            {
                Nomination nomination = await nominationGenericRepository.ReadSingle(nominationId);
                if (nomination == null) return Map.GetModelResult<NominationWithoutDelegatesDTO>(null, null, false, "Nomination Not Found");

                var thisNomination = Map.PeoplesNominationWithoutDelegates(nomination);
                return Map.GetModelResult(thisNomination, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region crud
        public async Task<ModelResult<NominationDTOw>> GetAllNominations()
        {
            try
            {
                var nominations = await nominationGenericRepository.ReadAll();
                if (!nominations.Any()) return Map.GetModelResult<NominationDTOw>(null, null, false, "No Nomination Found");

                var nominationList = Map.PeoplesNomination(nominations);
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

                var thisNomination = Map.PeoplesNomination(nomination);
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
                Nomination newNomination = new()
                {
                    Id = nomination.Id,
                    GroupId = nomination.GroupId,
                    Group = nomination.Group,
                    Counsellors = nomination.Counsellors,
                    PeoplesWarden = nomination.PeoplesWarden,
                    Delegates = nomination.Delegates
                };

                await nominationGenericRepository.Create(newNomination);
                await nominationGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Nomination Created");
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
                var checkNomination = await nominationGenericRepository.ReadSingle(nominationId);
                if (checkNomination == null) return Map.GetModelResult<string>(null, null, false, "Nomination Not Found");

                Nomination newNomination = new()
                {
                    Id = nomination.Id,
                    GroupId = nomination.GroupId,
                    Group = nomination.Group,
                    Counsellors = nomination.Counsellors,
                    PeoplesWarden = nomination.PeoplesWarden,
                    Delegates = nomination.Delegates
                };

                nominationGenericRepository.Update(newNomination);
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
