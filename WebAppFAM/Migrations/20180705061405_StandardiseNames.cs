using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class StandardiseNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TrailerTypes_TypeID",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Driver",
                table: "Driver");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Driver",
                newName: "Drivers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Vehicles",
                newName: "VehicleID");

            migrationBuilder.RenameColumn(
                name: "TypeID",
                table: "Vehicles",
                newName: "TrailerTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_TypeID",
                table: "Vehicles",
                newName: "IX_Vehicles_TrailerTypeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TrailerTypes",
                newName: "TrailerTypeID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Drivers",
                newName: "DriverID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrailerTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "VehicleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers",
                column: "DriverID");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drivers",
                table: "Drivers");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Drivers",
                newName: "Driver");

            migrationBuilder.RenameColumn(
                name: "VehicleID",
                table: "Vehicle",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TrailerTypeID",
                table: "Vehicle",
                newName: "TypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_TrailerTypeID",
                table: "Vehicle",
                newName: "IX_Vehicle_TypeID");

            migrationBuilder.RenameColumn(
                name: "TrailerTypeID",
                table: "TrailerTypes",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DriverID",
                table: "Driver",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TrailerTypes",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Driver",
                table: "Driver",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TrailerTypes_TypeID",
                table: "Vehicle",
                column: "TypeID",
                principalTable: "TrailerTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
