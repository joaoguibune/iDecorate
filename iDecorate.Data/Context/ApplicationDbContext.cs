using iDecorate.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace iDecorate.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) => Database.EnsureCreated();

        public DbSet<TopicEntity> topic { get; set; }
        public DbSet<WordEntity> word { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicEntity>().ToTable("topic")
                .HasMany(c => c.words)
                    .WithOne(e => e.topic);
            modelBuilder.Entity<WordEntity>().ToTable("word");
        }
    }
}