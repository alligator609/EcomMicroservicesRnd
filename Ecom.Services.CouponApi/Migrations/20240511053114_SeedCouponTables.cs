using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.Services.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "Id", "Code", "DiscountAmount", "LastUpdated", "MinAmount" },
                values: new object[,]
                {
                    { 1, "10OFF", 10.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 2, "20OFF", 20.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200 },
                    { 3, "30OFF", 30.0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
