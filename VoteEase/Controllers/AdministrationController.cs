using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.Application.Authorization;
using VoteEase.Application.Data;
using VoteEase.Application.Error;
using VoteEase.DTO.ReadDTO;

namespace VoteEase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly UserManager<VoteEaseUser> userManager;
        private readonly IAdminService adminService;
        private readonly IErrorService errorService;

        public AdministrationController(UserManager<VoteEaseUser> userManager,
                                        IAdminService adminService,
                                        IErrorService errorService)
        {
            this.userManager = userManager;
            this.adminService = adminService;
            this.errorService = errorService;
        }

        [HttpGet]
        [Route("roles/get-all")]
        public IActionResult GetAllRoles()
        {
            try
            {
                List<RoleDTO> roles = adminService.GetAllRoles();

                return Ok(new JsonMessage<RoleDTO>()
                {
                    Status = true,
                    Results = roles
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("roles/get-role-by-id/{roleId}")]
        public async Task<IActionResult> GetRoleById([FromRoute] string roleId)
        {
            try
            {
                RoleDTO role = await adminService.GetRoleById(roleId);

                if (role == null) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = "Role cannot be found."
                });

                return Ok(new JsonMessage<RoleDTO>()
                {
                    Status = true,
                    Results = new List<RoleDTO> { role }
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("roles/get-role-by-name/{roleName}")]
        public async Task<IActionResult> GetRoleByName([FromRoute] string roleName)
        {
            try
            {
                RoleDTO role = await adminService.GetRoleById(roleName);

                if (role == null) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = "Role cannot be found."
                });

                return Ok(new JsonMessage<RoleDTO>()
                {
                    Status = true,
                    Results = new List<RoleDTO> { role }
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }

        [HttpGet]
        [Route("roles/get-users-in-role/{roleNames}")]
        public async Task<IActionResult> GetUsersInRoles(string roleNames)
        {
            try
            {
                List<UsersInRoleDTO> roles = await adminService.GetUsersInRolesAsync(roleNames);

                return Ok(new JsonMessage<UsersInRoleDTO>()
                {
                    Status = false,
                    Results = roles
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}
