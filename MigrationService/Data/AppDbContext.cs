using Microsoft.EntityFrameworkCore;
using MigrationService.Models;

namespace MigrationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currency { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Rate).HasColumnType("numeric(18,6)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(255);
            });
        }


    }
}
