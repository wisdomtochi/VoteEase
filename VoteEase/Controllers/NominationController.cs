using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Error;
using VoteEase.Application.Votings;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominationController : ControllerBase
    {
        private readonly INominationService nominationService;
        private readonly IErrorService errorService;

        public NominationController(INominationService nominationService, IErrorService errorService)
        {
            this.nominationService = nominationService;
            this.errorService = errorService;
        }

        #region ADD NEW NOMINATION
        /// <summary>
        /// create a new nomination
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("nomination/create")]
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
        [Route("nomination/delete/{nominationId}")]
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
        [Route("nominations/get-all")]
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
        [Route("nomination/get/{nominationId}")]
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
        [Route("nomination/update/{nominationId}")]
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
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion
    }
}
