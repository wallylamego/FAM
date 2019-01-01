using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class AddStatusAndSubContratorTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubContractorID",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StatusID",
                table: "Trips",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SubContractorID",
                table: "Trips",
                column: "SubContractorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Status_StatusID",
                table: "Trips",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_SubContractor_SubContractorID",
                table: "Trips",
                column: "SubContractorID",
                principalTable: "SubContractor",
                principalColumn: "SubContractorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Status_StatusID",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_SubContractor_SubContractorID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_StatusID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_SubContractorID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SubContractorID",
                table: "Trips");
        }
    }
}
