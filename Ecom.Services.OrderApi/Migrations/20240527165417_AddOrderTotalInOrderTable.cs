﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Services.OrderApi.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTotalInOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CartTotal",
                table: "OrderHeaders",
                newName: "OrderTotal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderTotal",
                table: "OrderHeaders",
                newName: "CartTotal");
        }
    }
}
