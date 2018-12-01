using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class TrailerTypesTake5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles",
                column: "TrailerTypeID",
                principalTable: "TrailerTypes",
                principalColumn: "TrailerTypeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles",
                column: "TrailerTypeID",
                principalTable: "TrailerTypes",
                principalColumn: "TrailerTypeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
