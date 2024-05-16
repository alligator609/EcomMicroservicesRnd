using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Services.ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class AddsProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Smart Phone", "This is the description of iPhone 12", "product-1.png", "IPhone 12", 1000.0 },
                    { 2, "Smart Phone", "This is the description of Samsung Galaxy S20", "product-2.png", "Samsung Galaxy S20", 900.0 },
                    { 3, "Smart Phone", "This is the description of Huawei P30", "product-3.png", "Huawei P30", 800.0 },
                    { 4, "Smart Phone", "This is the description of Xiaomi Mi 10", "product-4.png", "Xiaomi Mi 10", 700.0 },
                    { 5, "Smart Phone", "This is the description of OnePlus 8", "product-5.png", "OnePlus 8", 600.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
