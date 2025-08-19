using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eCommerce_backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Footwear",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Footwear", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Footwear",
                columns: new[] { "Id", "Brand", "Color", "Description", "ImageUrl", "Name", "Price", "Size", "Stock" },
                values: new object[,]
                {
                    { 1, "Nike", "White", "Classic Nike Air Max 90 in white.", "https://example.com/nike-airmax90-white.jpg", "Air Max 90", 129.99m, "42", 10 },
                    { 2, "Adidas", "Black", "Adidas Ultraboost 22 for running and comfort.", "https://example.com/adidas-ultraboost22-black.jpg", "Ultraboost 22", 149.99m, "43", 15 },
                    { 3, "Converse", "Red", "Converse Chuck Taylor All Star classic sneakers.", "https://example.com/converse-chucktaylor-red.jpg", "Chuck Taylor All Star", 69.99m, "41", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Footwear");
        }
    }
}
