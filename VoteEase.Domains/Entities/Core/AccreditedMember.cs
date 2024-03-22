using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domain.Entities.Core
{
    public class AccreditedMember
    {
        [Key]
        public Guid MemberId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
