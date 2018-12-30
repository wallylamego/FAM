using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class DateTimeStampForFuelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderID",
                table: "FuelItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "FuelItems",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "FuelItems");

            migrationBuilder.AlterColumn<string>(
                name: "PurchaseOrderID",
                table: "FuelItems",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
