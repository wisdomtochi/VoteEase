using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteEase.Domains.Entities
{
    public class Nomination
    {
        [Key]
        public int Id { get; set; }
        public Guid GroupId { get; set; }
        [Required]
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        [Required]
        public Member CounsellorOne { get; set; }
        [Required]
        public Member CounsellorTwo { get; set; }
        [Required]
        public Member CounsellorThree { get; set; }
        public Member? PeoplesWarden { get; set; }
    }
}
