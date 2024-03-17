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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService groupService;
        private readonly IErrorService errorService;

        public GroupController(IGroupService groupService, IErrorService errorService)
        {
            this.groupService = groupService;
            this.errorService = errorService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetGroups()
        {
            try
            {
                var groups = await groupService.GetAllGroups();
                if (!groups.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = groups.Message
                });

                return Ok(new JsonMessage<GroupDTO>()
                {
                    Status = true,
                    Results = groups.ListOfEntities
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("get-group/{groupId}")]
        public async Task<IActionResult> GetGroup([FromRoute] Guid groupId)
        {
            try
            {
                var group = await groupService.GetGroup(groupId);
                if (!group.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = group.Message
                });

                return Ok(new JsonMessage<GroupDTO>()
                {
                    Status = group.Succeeded,
                    Result = group.Entity
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("create-new-group")]
        public async Task<IActionResult> AddNewGroup([FromBody] Group group)
        {
            try
            {
                var newGroup = await groupService.CreateNewGroup(group);
                if (!newGroup.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newGroup.Message
                });

                return Ok(new JsonMessage<GroupDTO>()
                {
                    Status = newGroup.Succeeded,
                    SuccessMessage = newGroup.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("update-group/{groupId}")]
        public async Task<IActionResult> UpdateGroup([FromBody] Group group, [FromRoute] Guid groupId)
        {
            try
            {
                var newGroupDetails = await groupService.UpdateGroupDetails(group, groupId);
                if (!newGroupDetails.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = newGroupDetails.Message
                });

                return Ok(new JsonMessage<GroupDTO>()
                {
                    Status = newGroupDetails.Succeeded,
                    SuccessMessage = newGroupDetails.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("delete-group")]
        public async Task<IActionResult> DeleteGroup([FromRoute] Guid groupId)
        {
            try
            {
                var group = await groupService.DeleteGroup(groupId);
                if (!group.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = group.Message
                });

                return Ok(new JsonMessage<GroupDTO>()
                {
                    Status = group.Succeeded,
                    SuccessMessage = group.Message
                });
            }
            catch (Exception e)
            {
                errorService.LogError(e);
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
