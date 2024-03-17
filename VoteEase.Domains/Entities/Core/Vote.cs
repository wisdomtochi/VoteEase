using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domain.Entities.Core
{
    public class Vote
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Member Member { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public Member Counsellor { get; set; }
        public Member PeoplesWarden { get; set; }
        public Member SynodDelegate { get; set; }
    }
}
