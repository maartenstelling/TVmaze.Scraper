using Microsoft.EntityFrameworkCore;

namespace TVmaze.Scraper.Persistence.Entities
{
    public class TVmazeContext : DbContext
    {
        public TVmazeContext(DbContextOptions options)
            : base(options) { }

        public DbSet<ShowEntity> Shows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainerName("shows");
            modelBuilder.Entity<ShowEntity>().OwnsMany(show => show.Cast);
        }
    }
}
