using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class CustomerTableDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Customers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Customers");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Customers",
                rowVersion: true,
                nullable: true);
        }
    }
}
