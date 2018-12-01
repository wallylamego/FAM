using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class TrailerTypeNewForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "TrailerTypeId",
                table: "Vehicles",
                newName: "TrailerTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_TrailerTypeId",
                table: "Vehicles",
                newName: "IX_Vehicles_TrailerTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles",
                column: "TrailerTypeID",
                principalTable: "TrailerTypes",
                principalColumn: "TrailerTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "TrailerTypeID",
                table: "Vehicles",
                newName: "TrailerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_TrailerTypeID",
                table: "Vehicles",
                newName: "IX_Vehicles_TrailerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeId",
                table: "Vehicles",
                column: "TrailerTypeId",
                principalTable: "TrailerTypes",
                principalColumn: "TrailerTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
