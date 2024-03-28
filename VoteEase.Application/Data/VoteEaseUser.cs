using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace VoteEase.Application.Data
{
    public class VoteEaseUser : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassCode { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
