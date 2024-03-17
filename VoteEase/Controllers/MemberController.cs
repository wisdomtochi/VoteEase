using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Error;
using VoteEase.Application.Helpers;
using VoteEase.Application.Votings;
using VoteEase.Domain.Entities.Core;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IErrorService errorService;

        public MemberController(IMemberService memberService, IErrorService errorService)
        {
            this.memberService = memberService;
            this.errorService = errorService;
        }

        #region ADD MEMBERS FROM EXCEL DOCUMENT
        [HttpPost]
        [Route("add-new-members/from-excel-file")]
        public async Task<IActionResult> AddNewMembersFromDoc([FromForm] ExcelSheetDTOw model)
        {
            try
            {
                ExcelReader excelReader = new ExcelReader(model.file);
                var result = excelReader.ReadMembersFromExcel();

                if (!result.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = result.Message
                });

                var newMembers = await memberService.AddMembersFromExcel(result.ListOfEntities);

                if (result.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = newMembers.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newMembers.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var members = await memberService.GetAllMembers();
                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = true,
                    Results = members.ListOfEntities
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("get-member/{memberId}")]
        public async Task<IActionResult> GetMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await memberService.GetMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = true,
                    Result = member.Entity
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("create-new-member")]
        public async Task<IActionResult> AddNewMember([FromBody] Member member)
        {
            try
            {
                var newMember = await memberService.CreateNewMember(member);
                if (!newMember.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newMember.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = newMember.Succeeded,
                    SuccessMessage = newMember.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("update-member/{memberId}")]
        public async Task<IActionResult> UpdateMember([FromBody] Member member, [FromRoute] Guid memberId)
        {
            try
            {
                var newMemberDetails = await memberService.UpdateMember(member, memberId);
                if (!newMemberDetails.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newMemberDetails.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = newMemberDetails.Succeeded,
                    SuccessMessage = newMemberDetails.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("delete-member")]
        public async Task<IActionResult> DeleteMember([FromRoute] Guid memberId)
        {
            try
            {
                var member = await memberService.DeleteMember(memberId);
                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<MemberDTO>()
                {
                    Status = member.Succeeded,
                    SuccessMessage = member.Message
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
