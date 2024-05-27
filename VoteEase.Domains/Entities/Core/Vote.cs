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
        public Guid NominationId { get; set; }
        [ForeignKey("NominationId")]
        public Nomination VotedPerson { get; set; }
        [Required]
        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Voter { get; set; }
        [Required]
        public Category Category { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
