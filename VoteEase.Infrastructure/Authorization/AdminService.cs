using VoteEase.Application.Authorization;
using VoteEase.Data_Access.Interface;
using VoteEase.Domain.Entities.Auth;

namespace VoteEase.Infrastructure.Authorization
{
    public class AdminService : IAdminService
    {
        private readonly IGenericRepository<Admin> adminGenericRepository;

        public AdminService(IGenericRepository<Admin> adminGenericRepository)
        {
            this.adminGenericRepository = adminGenericRepository;
        }


    }
}
