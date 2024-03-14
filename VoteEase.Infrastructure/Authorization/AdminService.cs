using VoteEase.Application.Authorization;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Auth;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;
using VoteEase.Mapper.Map;

namespace VoteEase.Infrastructure.Authorization
{
    public class AdminService : IAdminService
    {
        private readonly IGenericRepository<Admin> adminGenericRepository;

        public AdminService(IGenericRepository<Admin> adminGenericRepository)
        {
            this.adminGenericRepository = adminGenericRepository;
        }

        public async Task<ModelResult<AdminDTOw>> ReadAll()
        {
            try
            {
                var admins = await adminGenericRepository.ReadAll();
                if (!admins.Any()) return Map.GetModelResult<AdminDTOw>(null, null, false, "No Admin Found.");

                var adminList = Map.Admin(admins);

                return Map.GetModelResult(null, adminList, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<AdminDTOw>> ReadSingle(Guid adminId)
        {
            try
            {
                var admin = await adminGenericRepository.ReadSingle(adminId);
                if (admin == null) return Map.GetModelResult<AdminDTOw>(null, null, false, "Admin Not Found.");

                var thisAdmin = Map.Admin(admin);
                return Map.GetModelResult(thisAdmin, null, true, string.Empty);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ModelResult<string>> CreateAdmin(Admin admin)
        {
            try
            {
                var checkAdmin = await adminGenericRepository.ReadSingle(admin.Id);
                if (checkAdmin != null) return Map.GetModelResult<string>(null, null, false, "Admin Already Exists.");

                Admin newAdmin = new()
                {
                    Id = admin.Id,
                    Position = admin.Position,
                    EmailAddress = admin.EmailAddress
                };

                await adminGenericRepository.Create(newAdmin);
                await adminGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Admin Created Successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> DeleteAdmin(Guid adminId)
        {
            try
            {
                var checkAdmin = await adminGenericRepository.ReadSingle(adminId);
                if (checkAdmin == null) return Map.GetModelResult<string>(null, null, false, "Admin Not Found.");


                await adminGenericRepository.Delete(adminId);
                await adminGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Admin Deleted.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ModelResult<string>> UpdateAdmin(Guid adminId, Admin admin)
        {
            try
            {
                var checkAdmin = await adminGenericRepository.ReadSingle(adminId);
                if (checkAdmin == null) return Map.GetModelResult<string>(null, null, false, "Admin Not Found.");

                Admin updateAdmin = new()
                {
                    Id = admin.Id,
                    Position = admin.Position,
                    EmailAddress = admin.EmailAddress
                };

                adminGenericRepository.Update(updateAdmin);
                await adminGenericRepository.SaveChanges();
                return Map.GetModelResult<string>(null, null, true, "Admin Details Updated.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
