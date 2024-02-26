using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domains.Entities
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
        [Required]
        public bool IsAccredited { get; set; }
    }
}
