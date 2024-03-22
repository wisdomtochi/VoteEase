using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoteEase.Domain.Enums;

namespace VoteEase.Domain.Entities.Core
{
    public class Nomination
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public Category Category { get; set; }
        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
