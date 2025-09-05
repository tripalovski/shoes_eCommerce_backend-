using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce_backend.Migrations
{
    /// <inheritdoc />
    public partial class UserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "User",
                newName: "Name");
        }
    }
}
