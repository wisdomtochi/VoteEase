using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteEase.Domain.Entities.Core
{
    public class AccreditedMember
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
