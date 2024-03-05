using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domain.Entities.Auth
{
    public class Admin
    {
        [Required]
        public string Position { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
