using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class AddTraiilers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TrailerType_TypeID",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrailerType",
                table: "TrailerType");

            migrationBuilder.RenameTable(
                name: "TrailerType",
                newName: "TrailerTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrailerTypes",
                table: "TrailerTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TrailerTypes_TypeID",
                table: "Vehicle",
                column: "TypeID",
                principalTable: "TrailerTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TrailerTypes_TypeID",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrailerTypes",
                table: "TrailerTypes");

            migrationBuilder.RenameTable(
                name: "TrailerTypes",
                newName: "TrailerType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrailerType",
                table: "TrailerType",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TrailerType_TypeID",
                table: "Vehicle",
                column: "TypeID",
                principalTable: "TrailerType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
