using System.ComponentModel.DataAnnotations;

namespace VoteEase.Domains.Entities
{
    public class Vote
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Member Member { get; set; }
        [Required]
        public Member Counsellor { get; set; }
        public Member PeoplesWarden { get; set; }
    }
}
