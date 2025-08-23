using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandRelationToFootwear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Footwear");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Footwear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 1,
                column: "BrandId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 2,
                column: "BrandId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 3,
                column: "BrandId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Footwear_BrandId",
                table: "Footwear",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Footwear_Brand_BrandId",
                table: "Footwear",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Footwear_Brand_BrandId",
                table: "Footwear");

            migrationBuilder.DropIndex(
                name: "IX_Footwear_BrandId",
                table: "Footwear");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Footwear");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Footwear",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 1,
                column: "Brand",
                value: "Nike");

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 2,
                column: "Brand",
                value: "Adidas");

            migrationBuilder.UpdateData(
                table: "Footwear",
                keyColumn: "Id",
                keyValue: 3,
                column: "Brand",
                value: "Converse");
        }
    }
}
