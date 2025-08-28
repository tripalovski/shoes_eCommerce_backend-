using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenamedOrderDateCollumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "Order",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Order",
                newName: "CreatedAtDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Order",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "CreatedAtDate",
                table: "Order",
                newName: "OrderDate");
        }
    }
}
