using Microsoft.EntityFrameworkCore;
using WarehouseManager.Api.Models;

namespace WarehouseManager.Api.Data
{
    public class WarehouseDbContext: DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("citext");

            modelBuilder.Entity<Product>()
                .Property(p => p.SKU)
                .HasColumnType("citext");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Products_SKU_Length",
                    "char_length(\"SKU\") <= 30"
                ));
        }

    }
}
