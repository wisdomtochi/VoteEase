using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteEase.Domain.Entities.Core
{
    public class Group
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Guid LeaderId { get; set; }
        [Required]
        [ForeignKey("LeaderId")]
        public Member Leader { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
