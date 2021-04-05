using KnowledgeAccountinSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeAccountinSystem.Data
{
    public class KnowledgeAccountinSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Programmer> Programmers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public KnowledgeAccountinSystemContext() { }

        public KnowledgeAccountinSystemContext(DbContextOptions<KnowledgeAccountinSystemContext> options) : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.EnableSensitiveDataLogging();
        }
    }
}
