using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoteEase.Domains.Entities
{
    public class Nomination
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        [Required]
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public CounsellorCategory Counsellors { get; set; }
        public PeoplesWardenCategory? PeoplesWarden { get; set; }
        public DelegatesCategory? Delegates { get; set; }
    }

    public class CounsellorCategory
    {
        public Member CounsellorOne { get; set; }
        public Member CounsellorTwo { get; set; }
        public Member CounsellorThree { get; set; }
    }


    public class PeoplesWardenCategory
    {
        public Member PeoplesWarden { get; set; }
    }

    public class DelegatesCategory
    {
        public Member Delegate { get; set; }
    }
}
