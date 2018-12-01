using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class AddAbstractClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Horse",
                table: "Horse");

            migrationBuilder.RenameTable(
                name: "Horse",
                newName: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "HorseNo",
                table: "Vehicle",
                newName: "FleetNo");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Vehicle",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Horse");

            migrationBuilder.RenameColumn(
                name: "FleetNo",
                table: "Horse",
                newName: "HorseNo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Horse",
                table: "Horse",
                column: "ID");
        }
    }
}
