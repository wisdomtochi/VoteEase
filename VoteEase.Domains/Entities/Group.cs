using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteEase.Domains.Entities
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
    }
}
