using System.ComponentModel.DataAnnotations;

namespace VoteEase.API.Models
{
    public class RemoveUserFromRoleViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
