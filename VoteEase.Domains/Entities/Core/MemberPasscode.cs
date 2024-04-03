using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domain.Entities.Core
{
    public class MemberPasscode
    {
        [Key]
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string PassCode { get; set; }
    }
}
