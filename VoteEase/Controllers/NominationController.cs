using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Domains.Entities;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Infrastructure.Error;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominationController : ControllerBase
    {
        private readonly INominationService nominationService;

        public NominationController(INominationService nominationService)
        {
            this.nominationService = nominationService;
        }

        #region ADD NEW NOMINATION
        /// <summary>
        /// create a new nomination
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-new-nomination")]
        public async Task<IActionResult> AddNewNomination([FromBody] Nomination model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var nomination = await nominationService.CreateNewNomination(model);
                    if (!nomination.Succeeded) return Ok(new JsonMessage<string>()
                    {
                        Status = false,
                        ErrorMessage = nomination.Message
                    });

                    return Ok(new JsonMessage<NominationDTOw>()
                    {
                        Status = true,
                        SuccessMessage = nomination.Message
                    });
                }

                return BadRequest(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = "Please Insert values for the respective fields"
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region DELETE NOMINATION
        /// <summary>
        /// delete nomination
        /// </summary>
        /// <param name="nominationId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-nomination/{nominationId}")]
        public async Task<IActionResult> DeleteNomination([FromRoute] Guid nominationId)
        {
            try
            {
                var nomination = await nominationService.DeleteNomination(nominationId);

                if (!nomination.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nomination.Message
                });

                return Ok(new JsonMessage<NominationDTOw>()
                {
                    Status = true,
                    SuccessMessage = nomination.Message
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region READ ALL NOMINATIONS
        /// <summary>
        /// Get all nominations 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllNominations()
        {
            try
            {
                var nominations = await nominationService.GetAllNominations();
                if (!nominations.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nominations.Message
                });

                return Ok(new JsonMessage<NominationDTOw>()
                {
                    Status = true,
                    Results = nominations.ListOfEntities,
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region READ SPECIFIC NOMINATION
        /// <summary>
        /// get specific nomination
        /// </summary>
        /// <param name="nominationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("nomination/{nominationId}")]
        public async Task<IActionResult> GetNomination([FromRoute] Guid nominationId)
        {
            try
            {
                var nomination = await nominationService.GetNomination(nominationId);
                if (!nomination.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nomination.Message
                });

                return Ok(new JsonMessage<NominationDTOw>()
                {
                    Status = true,
                    Result = nomination.Entity
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region READ ONLY THE NOMINEES
        /// <summary>
        /// get only the nominated persons
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("nominees")]
        public async Task<IActionResult> GetNominees()
        {
            try
            {
                var nominee = await nominationService.GetNominatedPersons();
                if (!nominee.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nominee.Message
                });

                return Ok(new JsonMessage<NominationDTO>()
                {
                    Status = true,
                    Result = nominee.Entity
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region READ ALL NOMINATIONS WITHOUT SYNOD DELEGATES
        /// <summary>
        /// get all nominations that don't have synod delegates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-nominations")]
        public async Task<IActionResult> GetNominationsWithoutDelegates()
        {
            try
            {
                var nominations = await nominationService.GetNominationsWithoutDelegates();
                if (!nominations.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nominations.Message
                });

                return Ok(new JsonMessage<NominationWithoutDelegatesDTO>()
                {
                    Status = true,
                    Results = nominations.ListOfEntities
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region READ SPECIFIC NOMINATION WITHOUT SYNOD DELEGATE
        [HttpGet]
        [Route("get-all-nominations/{nominationId}")]
        public async Task<IActionResult> GetNominationWithoutDelegate(Guid Id)
        {
            try
            {
                var nomination = await nominationService.GetSpecificNominationWithoutDelegate(Id);
                if (!nomination.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = nomination.Message
                });

                return Ok(new JsonMessage<NominationWithoutDelegatesDTO>()
                {
                    Status = true,
                    Result = nomination.Entity
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region UPDATE NOMINATION
        /// <summary>
        /// update nomination details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="nominationId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-nomination/{nominationId}")]
        public async Task<IActionResult> UpdateNomination([FromBody] Nomination model, [FromRoute] Guid nominationId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var nomination = await nominationService.UpdateNomination(model, nominationId);
                    if (!nomination.Succeeded) return Ok(new JsonMessage<string>()
                    {
                        Status = false,
                        ErrorMessage = nomination.Message
                    });

                    return Ok(new JsonMessage<NominationDTOw>()
                    {
                        Status = true,
                        SuccessMessage = nomination.Message
                    });
                }

                return BadRequest(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = "Please Insert values for the respective fields"
                });
            }
            catch (Exception e)
            {
                ErrorService errorService = new();
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion
    }
}
