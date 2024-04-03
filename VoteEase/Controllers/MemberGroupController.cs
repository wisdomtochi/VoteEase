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
    public class MemberGroupController : ControllerBase
    {
        private readonly IMemberInGroupService memberInGroupService;
        private readonly IErrorService errorService;

        public MemberGroupController(IMemberInGroupService memberInGroupService,
                                     IErrorService errorService)
        {
            this.memberInGroupService = memberInGroupService;
            this.errorService = errorService;
        }

        [HttpGet]
        [Route("member_groups/member/{memberId}/{groupId}")]
        public async Task<IActionResult> GetMember([FromRoute] Guid memberId, [FromRoute] Guid groupId)
        {
            try
            {
                var member = await memberInGroupService.GetMemberInGroup(memberId, groupId);

                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<MemberInGroupDTOw>()
                {
                    Status = true,
                    Result = member.Entity
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("member_groups/get-all")]
        public async Task<IActionResult> GetAllMembers()
        {
            try
            {
                var members = await memberInGroupService.GetAllMembersAndTheirGroups();

                if (!members.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = members.Message
                });

                return Ok(new JsonMessage<MemberInGroupDTOw>()
                {
                    Status = true,
                    Results = members.ListOfEntities
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("member_groups/add-member-to-group")]
        public async Task<IActionResult> AddMemberToGroup([FromBody] MemberInGroup memberInGroup)
        {
            try
            {
                var member = await memberInGroupService.AddNewMemberToGroup(memberInGroup);

                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("member_groups/update-member-in-group/{memberId}/{groupId}")]
        public async Task<IActionResult> UpdateMemberInGroup([FromRoute] Guid memberId, [FromRoute] Guid groupId, [FromBody] MemberInGroup memberInGroup)
        {
            try
            {
                var member = await memberInGroupService.UpdateMemberInGroup(memberId, groupId, memberInGroup);

                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("member_groups/delete-member-in-group")]
        public async Task<IActionResult> DeleteMemberInGroup(Guid memberId, Guid groupId)
        {
            try
            {
                var member = await memberInGroupService.DeleteMemberInGroup(memberId, groupId);

                if (!member.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = member.Message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = member.Message
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
