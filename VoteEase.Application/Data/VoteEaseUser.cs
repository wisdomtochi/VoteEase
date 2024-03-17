using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VoteEase.Application.Data
{
    public class VoteEaseUser : IdentityUser
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PassCode { get; set; }
    }
}
