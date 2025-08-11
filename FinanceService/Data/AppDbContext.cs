using Microsoft.EntityFrameworkCore;
using FinanceService.Models;

namespace FinanceService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currency { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Rate).HasColumnType("numeric(18,6)").IsRequired();
            });
        }
    }
}
