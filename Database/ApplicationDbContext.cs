using eCommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Footwear> Footwear { get; set; }
        public DbSet<Models.Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Configure Footwear entity
            modelBuilder.Entity<Footwear>(entity => {
                entity.HasKey(f => f.Id); // Primary key

                entity.Property(f => f.Name)
                      .IsRequired()
                      .HasMaxLength(100); // Limit length of name

                entity.Property(f => f.Price)
                      .HasColumnType("decimal(18,2)"); // Precision for price

                entity.Property(f => f.Color)
                      .HasMaxLength(30);

                entity.Property(f => f.Size)
                      .HasMaxLength(10);

                entity.Property(f => f.Description)
                      .HasMaxLength(500);

                entity.Property(f => f.ImageUrl)
                      .HasMaxLength(200);

                entity.HasOne(f => f.Brand)
                  .WithMany(b => b.Footwears)
                  .HasForeignKey(f => f.BrandId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Models.Brand>(entity => {
                entity.HasKey(b => b.Id); // Primary key
                entity.Property(b => b.Name)
                      .IsRequired()
                      .HasMaxLength(100); // Limit length of name
                entity.Property(b => b.Country)
                      .HasMaxLength(50);
                entity.Property(b => b.Description)
                      .HasMaxLength(500);
                entity.Property(b => b.Website)
                      .HasMaxLength(200);
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.FootwearId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Footwear)
                .WithMany(f => f.OrderItems)
                .HasForeignKey(oi => oi.FootwearId);
        

            // Seed initial test data
            modelBuilder.Entity<Models.Brand>().HasData(
                new Models.Brand {
                    Id = 1,
                    Name = "Nike",
                    Country = "USA",
                    Description = "Leading sportswear brand known for innovation and style.",
                    Website = "https://www.nike.com"
                },
                new Models.Brand {
                    Id = 2,
                    Name = "Adidas",
                    Country = "Germany",
                    Description = "Global brand offering a wide range of athletic footwear and apparel.",
                    Website = "https://www.adidas.com"
                },
                new Models.Brand {
                    Id = 3,
                    Name = "Puma",
                    Country = "Germany",
                    Description = "Renowned for its stylish and performance-oriented sportswear.",
                    Website = "https://www.puma.com"
                }
            );

            // Seed initial test data
            modelBuilder.Entity<Footwear>().HasData(
                new Footwear {
                    Id = 1,
                    Name = "Air Max 90",
                    BrandId = 1,
                    Price = 129.99m,
                    Color = "White",
                    Size = "42",
                    Stock = 10,
                    Description = "Classic Nike Air Max 90 in white.",
                    ImageUrl = "https://example.com/nike-airmax90-white.jpg"
                },
                new Footwear {
                    Id = 2,
                    Name = "Ultraboost 22",
                    BrandId = 2,
                    Price = 149.99m,
                    Color = "Black",
                    Size = "43",
                    Stock = 15,
                    Description = "Adidas Ultraboost 22 for running and comfort.",
                    ImageUrl = "https://example.com/adidas-ultraboost22-black.jpg"
                },
                new Footwear {
                    Id = 3,
                    Name = "Chuck Taylor All Star",
                    BrandId = 3,
                    Price = 69.99m,
                    Color = "Red",
                    Size = "41",
                    Stock = 20,
                    Description = "Converse Chuck Taylor All Star classic sneakers.",
                    ImageUrl = "https://example.com/converse-chucktaylor-red.jpg"
                }
            );

        }
    }
}
