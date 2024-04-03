using Microsoft.AspNetCore.Mvc;
using VoteEase.API.Helpers;
using VoteEase.API.Models;
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
        private readonly IAdminService adminService;
        private readonly IErrorService errorService;

        public AdministrationController(IAdminService adminService,
                                        IErrorService errorService)
        {
            this.adminService = adminService;
            this.errorService = errorService;
        }

        #region ROLE ADMINISTRATION

        #region GET ALL ROLES
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
        #endregion

        #region GET ROLE BY ID
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
        #endregion

        #region GET ROLE BY NAME
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
        #endregion

        #region GET USERS IN ROLES
        [HttpGet]
        [Route("roles/get-users-in-role/{roleNames}")]
        public async Task<IActionResult> GetUsersInRoles([FromRoute] string roleNames)
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
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region CREATE ROLE
        [HttpPost]
        [Route("role/create")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO model)
        {
            try
            {
                (bool status, string message) = await adminService.CreateRole(model.Name);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region UPDATE ROLE
        [HttpPost]
        [Route("role/update/{roleId}")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDTO model, [FromRoute] string roleId)
        {
            try
            {
                (bool status, string message) = await adminService.UpdateRole(model.Name, roleId);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region DELETE ROLE BY ID
        [HttpPost]
        [Route("role/delete/{roleId}")]
        public async Task<IActionResult> DeleteRoleById([FromRoute] string roleId)
        {
            try
            {
                (bool status, string message) = await adminService.DeleteRoleById(roleId);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region DELETE ROLE BY NAME
        [HttpPost]
        [Route("role/delete/{roleName}")]
        public async Task<IActionResult> DeleteRoleByName([FromRoute] string roleName)
        {
            try
            {
                (bool status, string message) = await adminService.DeleteRoleByName(roleName);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region ADD USER TO ROLE
        [HttpPost]
        [Route("role/add-to-role")]
        public async Task<IActionResult> AddToRole([FromBody] AddUserToRoleViewModel model)
        {
            try
            {
                (bool status, string message) = await adminService.AddToRole(model.UserId, model.RoleNames);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region REMOVE USER FROM ROLE
        [HttpPost]
        [Route("role/remove-from-role")]
        public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleViewModel model)
        {
            try
            {
                (bool status, string message) = await adminService.RemoveFromRole(model.UserId, model.RoleName);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region IS USER IN ROLE
        [HttpPost]
        [Route("role/is-user-in-role")]
        public async Task<IActionResult> IsUserInRole([FromBody] IsUserInRoleViewModel model)
        {
            try
            {
                (bool status, string message) = await adminService.IsUserInRole(model.UserId, model.RoleName);

                if (!status) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = message
                });

                return Ok(new JsonMessage<string>()
                {
                    Status = true,
                    SuccessMessage = message
                });

            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #endregion

        #region USERS

        #region CREATE USER
        [HttpPost]
        [Route("user/create")]
        public async Task<IActionResult> CreateAppUser(string FirstName, string LastName, string PassCode)
        {
            try
            {
                var user = await adminService.CreateAppUser(FirstName, LastName, PassCode);

                if (!user.Succeeded) return Ok(new JsonMessage<string>()
                {
                    Status = false,
                    ErrorMessage = user.Message
                });

                return Ok(new JsonMessage<VoteEaseUser>()
                {
                    Status = true,
                    Result = user.Entity
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region COUNT USERS
        [HttpGet]
        [Route("users/count")]
        public IActionResult CountUsers()
        {
            try
            {
                var users = adminService.CountUsers();

                return Ok(new JsonMessage<int>()
                {
                    Status = true,
                    Result = users
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region GET ALL USERS
        [HttpGet]
        [Route("users/get-all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await adminService.GetAllUsers();

                return Ok(new JsonMessage<VoteEaseUser>()
                {
                    Status = true,
                    Results = users
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region COUNT USERS IN ROLE
        [HttpGet]
        [Route("user/count-users-in-role")]
        public async Task<IActionResult> CountUsersInRole(string roleName)
        {
            try
            {
                var users = await adminService.CountUsersInRole(roleName);

                return Ok(new JsonMessage<int>()
                {
                    Status = true,
                    Result = users
                });
            }
            catch (Exception ex)
            {
                errorService.LogError(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #endregion
    }
}
