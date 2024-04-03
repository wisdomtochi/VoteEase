using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domain.Entities.Core
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public Group? Group { get; set; }
    }
}
