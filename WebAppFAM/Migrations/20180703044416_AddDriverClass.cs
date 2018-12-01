using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAppFAM.Migrations
{
    public partial class AddDriverClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CellNumber = table.Column<string>(nullable: false),
                    IDNumber = table.Column<string>(nullable: true),
                    MedicalExpiryDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    NextofKin = table.Column<string>(nullable: true),
                    NextofKinDate = table.Column<DateTime>(nullable: false),
                    PDPExpiryDate = table.Column<DateTime>(nullable: false),
                    PassportNo = table.Column<DateTime>(nullable: false),
                    SecondName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Driver");
        }
    }
}
