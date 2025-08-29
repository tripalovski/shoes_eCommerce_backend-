using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_backend.Migrations
{
    /// <inheritdoc />
    public partial class Fixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FootwearId1",
                table: "OrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_FootwearId1",
                table: "OrderItem",
                column: "FootwearId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Footwear_FootwearId1",
                table: "OrderItem",
                column: "FootwearId1",
                principalTable: "Footwear",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Footwear_FootwearId1",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_FootwearId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "FootwearId1",
                table: "OrderItem");
        }
    }
}
