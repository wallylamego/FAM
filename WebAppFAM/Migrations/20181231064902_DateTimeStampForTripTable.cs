using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class DateTimeStampForTripTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Trips",
                nullable: true);

            

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserID",
                table: "Trips",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_UserID",
                table: "Trips",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_ApplicationUser_UserID",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Trips");
        }
    }
}
