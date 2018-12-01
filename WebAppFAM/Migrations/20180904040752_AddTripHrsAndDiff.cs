using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class AddTripHrsAndDiff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualCollectionDateTime",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
  
            migrationBuilder.AddColumn<double>(
                name: "DiffCollectionTimeHrs",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiffEndTimeHrs",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiffStartTimeHrs",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpectedCollectionDateTime",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "InvoiceAmount",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNo",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "InvoicedKms",
                table: "Trips",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualCollectionDateTime",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DiffCollectionTimeHrs",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DiffEndTimeHrs",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DiffStartTimeHrs",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ExpectedCollectionDateTime",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "InvoiceNo",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "InvoicedKms",
                table: "Trips");
        }
    }
}
