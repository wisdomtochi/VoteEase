using VoteEase.Application.Data;
using VoteEase.DTO.ReadDTO;

namespace VoteEase.Application.Authorization
{
    public interface IAdminService
    {
        List<RoleDTO> GetAllRoles();
        Task<RoleDTO> GetRoleById(string roleId);
        Task<RoleDTO> GetRoleByName(string roleName);
        Task<List<UsersInRoleDTO>> GetUsersInRolesAsync(string rolenames);
        Task<(bool status, string message)> CreateRole(string roleName);
        Task<(bool status, string message)> UpdateRole(string roleId, string roleName);
        Task<(bool status, string message)> DeleteRoleById(string roleId);
        Task<(bool status, string message)> DeleteRoleByName(string roleName);
        Task<(bool status, string message)> AddToRole(List<string> users, List<string> roleNames);
        Task<(bool status, string message)> RemoveFromRole(string userId, string roleName);
        Task<(bool status, string message)> IsUserInRole(string userId, string roleName);
        Task<ModelResult<VoteEaseUser>> CreateAppUser(string firstName, string lastName, string passCode);
        int CountUsers();
        Task<List<VoteEaseUser>> GetAllUsers();
        Task<int> CountUsersInRole(string roleName);
    }
}
