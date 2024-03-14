using Microsoft.EntityFrameworkCore;
using VoteEase.Domain.Entities;
using VoteEase.Domain.Entities.Auth;
using VoteEase.Domain.Entities.Errors;
using VoteEase.Domains.Entities;

namespace VoteEase.Data.Context
{
    public class VoteEaseDbContext : DbContext
    {
        public VoteEaseDbContext(DbContextOptions<VoteEaseDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Nomination> Nominations { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AccreditedMember> AccreditedMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
