using HistoryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HistoryAPI.Repository.Contexts
{
    public class HistoryContext : DbContext
    {
        public DbSet<CarHistory> CarHistories { get; set; }

        public HistoryContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarHistory>().ToTable("CarHistory");
            modelBuilder.Entity<CarHistory>().HasKey(c => c.Id);
            modelBuilder.Entity<CarHistory>().Property(c => c.Company);
            modelBuilder.Entity<CarHistory>().Property(c => c.Model);
            modelBuilder.Entity<CarHistory>().Property(c => c.Year);
            modelBuilder.Entity<CarHistory>().Property(c => c.Price);
            modelBuilder.Entity<CarHistory>().Property(c => c.UserId);
            modelBuilder.Entity<CarHistory>().Property(c => c.Action);
        }
    }
}
