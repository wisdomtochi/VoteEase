using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
