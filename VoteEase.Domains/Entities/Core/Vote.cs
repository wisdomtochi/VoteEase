using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoteEase.Domain.Enums;

namespace VoteEase.Domain.Entities.Core
{
    public class Vote
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid VoterId { get; set; }
        [ForeignKey("VoterId")]
        public Member Voter { get; set; }
        [Required]
        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        [Required]
        public Category Category { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
