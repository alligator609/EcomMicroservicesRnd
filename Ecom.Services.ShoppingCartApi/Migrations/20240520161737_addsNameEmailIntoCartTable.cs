using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Services.ShoppingCartApi.Migrations
{
    /// <inheritdoc />
    public partial class addsNameEmailIntoCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CartHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CartHeaders");

            migrationBuilder.DropColumn(
                name: "FirstName", 
                table: "CartHeaders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "CartHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CartHeaders");
        }
    }
}
