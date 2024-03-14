using VoteEase.Domain.Entities.Auth;
using VoteEase.DTO.ReadDTO;
using VoteEase.DTO.WriteDTO;

namespace VoteEase.Application.Authorization
{
    public interface IAdminService
    {
        Task<ModelResult<AdminDTOw>> ReadAll();
        Task<ModelResult<AdminDTOw>> ReadSingle(Guid adminId);
        Task<ModelResult<string>> CreateAdmin(Admin admin);
        Task<ModelResult<string>> UpdateAdmin(Guid adminId, Admin admin);
        Task<ModelResult<string>> DeleteAdmin(Guid adminId);
    }
}
