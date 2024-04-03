using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoteEase.Application.Authorization;
using VoteEase.Application.Data;
using VoteEase.DTO.ReadDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Authorization
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<VoteEaseUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminService(UserManager<VoteEaseUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public List<RoleDTO> GetAllRoles()
        {
            List<IdentityRole> roles = roleManager.Roles.ToList();

            List<RoleDTO> RolesWithUsers = roles.Select(r => new RoleDTO()
            {
                Id = r.Id,
                Name = r.Name,
                UsersInRole = GetUsersInRolesAsync(r.Name).GetAwaiter().GetResult()
            }).ToList();

            return RolesWithUsers;
        }

        public async Task<List<UsersInRoleDTO>> GetUsersInRolesAsync(string rolename)
        {
            IList<VoteEaseUser> users = await userManager.GetUsersInRoleAsync(rolename);

            if (users == null) return null;

            List<UsersInRoleDTO> usersInRoles = users.Select(r => new UsersInRoleDTO()
            {
                Id = r.Id,
                FullName = $"{r.FirstName} {r.LastName}"
            }).ToList();

            return usersInRoles;
        }

        public async Task<RoleDTO> GetRoleById(string roleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);

            if (role == null) return null;

            RoleDTO usersInRole = new()
            {
                Id = role.Id,
                Name = role.Name,
                UsersInRole = await GetUsersInRolesAsync(role.Name)
            };

            return usersInRole;
        }

        public async Task<RoleDTO> GetRoleByName(string roleName)
        {
            IdentityRole role = await roleManager.FindByNameAsync(roleName);

            if (role == null) return null;

            RoleDTO usersInRole = new()
            {
                Id = role.Id,
                Name = role.Name,
                UsersInRole = await GetUsersInRolesAsync(role.Name)
            };

            return usersInRole;
        }

        public async Task<(bool status, string message)> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return (false, "Role cannot be empty.");

            var RoleExist = roleManager.FindByNameAsync(roleName);

            if (RoleExist != null) return (true, $"{roleName} already exist.");

            IdentityRole role = new() { Name = roleName };

            IdentityResult result = await roleManager.CreateAsync(role);

            if (result.Succeeded) return (true, "Role created successfully.");

            return (false, "Failed to create role.");
        }

        public async Task<(bool status, string message)> UpdateRole(string roleId, string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return (false, "Role cannot be empty.");

            var RoleExist = await roleManager.FindByIdAsync(roleId);

            if (RoleExist == null) return (false, "role to updated can not  be found.");

            RoleExist.Name = roleName;

            IdentityResult result = await roleManager.UpdateAsync(RoleExist);

            if (result.Succeeded) return (true, $"{roleName} Updated Successfully.");

            return (false, "Failed to update role.");
        }

        public async Task<(bool status, string message)> DeleteRoleByName(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return (false, "Role cannot be empty.");

            var RoleExist = await roleManager.FindByNameAsync(roleName);

            if (RoleExist == null) return (false, "Role does not exist.");

            IdentityResult result = await roleManager.DeleteAsync(RoleExist);

            if (result.Succeeded) return (true, "Role deleted successfully.");

            return (false, "Failed to delete role.");
        }

        public async Task<(bool status, string message)> DeleteRoleById(string roleId)
        {
            IdentityRole RoleExist = await roleManager.FindByIdAsync(roleId);

            if (RoleExist == null) return (false, "Role does not exist.");

            IdentityResult result = await roleManager.DeleteAsync(RoleExist);

            if (result.Succeeded) return (true, "Role deleted successfully.");

            return (false, "Failed to delete role.");
        }

        public async Task<List<VoteEaseUser>> GetAllUsers()
        {
            try
            {
                var users = await userManager.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ModelResult<VoteEaseUser>> CreateAppUser(string firstName, string lastName, string passCode)
        {
            try
            {
                VoteEaseUser newUser = new()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PassCode = passCode,
                    DateJoined = DateTime.UtcNow
                };

                IdentityResult result = await userManager.CreateAsync(newUser);

                if (!result.Succeeded) return Map.GetModelResult<VoteEaseUser>(null, null, false, "User Failed To Create.");

                return Map.GetModelResult(newUser, null, true, "User Created.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(bool status, string message)> AddToRole(List<string> users, List<string> roleNames)
        {
            List<VoteEaseUser> voteEaseUsers = new();

            foreach (var user in users)
            {
                var item = await userManager.FindByIdAsync(user);

                if (item == null) return (false, "User Not Found.");

                voteEaseUsers.Add(item);
            }

            List<IdentityRole> roles = roleManager.Roles.ToList();

            var existingRoles = roles.Select(x => new
            {
                name = x.Name
            }).ToList();

            if (voteEaseUsers.Count <= 0) return (false, "Users to add to role were not found.");

            foreach (var user in voteEaseUsers)
            {
                await userManager.AddToRolesAsync(user, roleNames);
            }

            return (true, "Users added to roles.");
        }

        public async Task<(bool status, string message)> IsUserInRole(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(roleName)) return (false, "User and Role Name cannot be empty.");

            VoteEaseUser voteEaseUser = await userManager.FindByIdAsync(userId);

            bool result = await userManager.IsInRoleAsync(voteEaseUser, roleName);

            if (result) return (true, string.Empty);

            return (false, $"{voteEaseUser.FirstName} {voteEaseUser.LastName} is not in {roleName}.");
        }

        public async Task<(bool status, string message)> RemoveFromRole(string userId, string roleName)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(roleName)) return (false, "User and Role Name cannot be empty.");

                bool status = await roleManager.RoleExistsAsync(roleName);

                if (!status) return (false, "Role does not exist.");

                var user = await userManager.FindByIdAsync(userId);

                if (user == null) return (false, "User does not exist.");

                var result = await userManager.RemoveFromRoleAsync(user, roleName);

                if (result.Succeeded) return (true, "User Removed From Role Successfully.");

                return (false, "Failed to remove user.");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CountUsers()
        {
            try
            {
                return userManager.Users.Count();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> CountUsersInRole(string roleName)
        {
            try
            {
                IList<VoteEaseUser> users = await userManager.GetUsersInRoleAsync(roleName);

                return users.Count;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
