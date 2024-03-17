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
    public class AccreditedMemberController : ControllerBase
    {
        private readonly IAccreditedMemberService accreditedMemberService;
        private readonly IErrorService errorService;

        public AccreditedMemberController(IAccreditedMemberService accreditedMemberService, IErrorService errorService)
        {
            this.accreditedMemberService = accreditedMemberService;
            this.errorService = errorService;
        }

        #region ADD LIST OF ACCREDITED MEMBERS
        [HttpPost]
        [Route("add-new-members")]
        public async Task<IActionResult> AddListOfNewAccreditedMember([FromBody] List<AccreditedMember> model)
        {
            try
            {
                var members = await accreditedMemberService.CreateListOfAccreditedMember(model);
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    SuccessMessage = members.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }
        #endregion

        #region DELETE LIST OF ACCREDITED MEMBERS
        public async Task<IActionResult> DeleteListOfAccreditedMembers(List<AccreditedMember> model)
        {
            try
            {
                var members = await accreditedMemberService.RemoveListOfAccreditedMember(model);
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    SuccessMessage = members.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAccreditedMembers()
        {
            try
            {
                var members = await accreditedMemberService.GetAccreditedMembers();
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    Results = members.ListOfEntities
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("get-member/{memberId}")]
        public async Task<IActionResult> GetAccreditedMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await accreditedMemberService.GetAccreditedMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    Result = member.Entity
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPost]
        [Route("add-membeer")]
        public async Task<IActionResult> AddNewAccreditedMember([FromBody] AccreditedMember model)
        {
            try
            {
                var member = await accreditedMemberService.CreateNewAccreditedMember(model);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPost]
        [Route("update-member/{memberId}")]
        public async Task<IActionResult> UpdateAccreditedMember([FromBody] AccreditedMember model, [FromRoute] Guid memberId)
        {
            try
            {
                var member = await accreditedMemberService.UpdateAccreditedMember(model, memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpPost]
        [Route("delete-member/{memberId}")]
        public async Task<IActionResult> DeleteAccreditedMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await accreditedMemberService.DeleteAccreditedMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<AccreditedMemberDTO>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }
        #endregion
    }
}
