using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class AddTraiilers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Driver");

            migrationBuilder.AddColumn<string>(
                name: "LinkRegistrationNumber",
                table: "Vehicle",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkVinNo",
                table: "Vehicle",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Vehicle",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Driver",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "IDNumber",
                table: "Driver",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CellNumber",
                table: "Driver",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Driver",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "TrailerType",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerType", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_TypeID",
                table: "Vehicle",
                column: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_TrailerType_TypeID",
                table: "Vehicle",
                column: "TypeID",
                principalTable: "TrailerType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_TrailerType_TypeID",
                table: "Vehicle");

            migrationBuilder.DropTable(
                name: "TrailerType");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_TypeID",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "LinkRegistrationNumber",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "LinkVinNo",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Driver");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Driver",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "IDNumber",
                table: "Driver",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CellNumber",
                table: "Driver",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Driver",
                nullable: false,
                defaultValue: "");
        }
    }
}
