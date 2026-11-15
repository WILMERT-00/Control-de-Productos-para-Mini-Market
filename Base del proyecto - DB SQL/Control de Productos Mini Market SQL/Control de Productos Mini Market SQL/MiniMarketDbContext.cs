
using Microsoft.EntityFrameworkCore;

namespace Control_de_Productos_Mini_Market_SQL
{
    public class MiniMarketDbContext : DbContext
    {
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=.\SQLEXPRESS;
                      Database=MiniMarketDB;
                      Trusted_Connection=True;
                      TrustServerCertificate=True;");
            }
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabla Categories
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.CategoryId);

                entity.Property(c => c.Name)
                    .HasColumnName("CategoryName")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(c => c.Name).IsUnique();
            });

            // Tabla Brands
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brands");
                entity.HasKey(b => b.BrandId);

                entity.Property(b => b.Name)
                    .HasColumnName("BrandName")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(b => b.Name).IsUnique();
            });

            // Tabla Products
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                    .HasColumnName("ProductName")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(p => p.Stock)
                    .IsRequired()
                    .HasDefaultValue(0);

                entity.HasIndex(p => p.Name).IsUnique();

                entity.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey("CategoryId");

                entity.HasOne(p => p.Brand)
                    .WithMany()
                    .HasForeignKey("BrandId");
            });
        }
    }
}
