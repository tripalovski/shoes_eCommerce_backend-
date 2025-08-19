using eCommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_backend.Database
{
    public class FootwearDbContext (DbContextOptions<FootwearDbContext> options) : DbContext(options)
    {
        // DbSet represents the Footwear table in the database
        public DbSet<Footwear> Footwear { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // Configure Footwear entity
            modelBuilder.Entity<Footwear>(entity => {
                entity.HasKey(f => f.Id); // Primary key

                entity.Property(f => f.Name)
                      .IsRequired()
                      .HasMaxLength(100); // Limit length of name

                entity.Property(f => f.Brand)
                      .IsRequired()
                      .HasMaxLength(50);

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
            });

            // Seed initial test data
            modelBuilder.Entity<Footwear>().HasData(
                new Footwear {
                    Id = 1,
                    Name = "Air Max 90",
                    Brand = "Nike",
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
                    Brand = "Adidas",
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
                    Brand = "Converse",
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
