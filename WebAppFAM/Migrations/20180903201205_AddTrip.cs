using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class AddTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Customers_CustomerID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Locations_EndLocationID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Locations_StartLocationID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

            migrationBuilder.CreateTable(
                name: "Commodity",
                columns: table => new
                {
                    CommodityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodity", x => x.CommodityID);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripCode = table.Column<string>(nullable: false),
                    ReverseDestinationID = table.Column<bool>(nullable: false),
                    DestinationID = table.Column<int>(nullable: false),
                    ReturnTrip = table.Column<bool>(nullable: false),
                    ReturnTripID = table.Column<int>(nullable: false),
                    DriverID = table.Column<int>(nullable: true),
                    HorseID = table.Column<int>(nullable: true),
                    TrailerID = table.Column<int>(nullable: true),
                    CommodityID = table.Column<int>(nullable: true),
                    ExpectedStartDateTime = table.Column<DateTime>(nullable: false),
                    ActualStartDateTime = table.Column<DateTime>(nullable: false),
                    ExpectedCompletionDateTime = table.Column<DateTime>(nullable: false),
                    ActualCompletionDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripID);
                    table.ForeignKey(
                        name: "FK_Trips_Commodity_CommodityID",
                        column: x => x.CommodityID,
                        principalTable: "Commodity",
                        principalColumn: "CommodityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Destinations_DestinationID",
                        column: x => x.DestinationID,
                        principalTable: "Destinations",
                        principalColumn: "DestinationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_HorseID",
                        column: x => x.HorseID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_TrailerID",
                        column: x => x.TrailerID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuelItems",
                columns: table => new
                {
                    FuelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripID = table.Column<int>(nullable: false),
                    PurchaseOrderID = table.Column<string>(nullable: true),
                    FuelRate = table.Column<double>(nullable: false),
                    Litres = table.Column<double>(nullable: false),
                    Odometre = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelItems", x => x.FuelID);
                    table.ForeignKey(
                        name: "FK_FuelItems_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kilometre",
                columns: table => new
                {
                    KilometreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripID = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    KmsTravelled = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kilometre", x => x.KilometreID);
                    table.ForeignKey(
                        name: "FK_Kilometre_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuelItems_TripID",
                table: "FuelItems",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Kilometre_TripID",
                table: "Kilometre",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CommodityID",
                table: "Trips",
                column: "CommodityID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationID",
                table: "Trips",
                column: "DestinationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverID",
                table: "Trips",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_HorseID",
                table: "Trips",
                column: "HorseID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TrailerID",
                table: "Trips",
                column: "TrailerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Customers_CustomerID",
                table: "Destinations",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Locations_EndLocationID",
                table: "Destinations",
                column: "EndLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Locations_StartLocationID",
                table: "Destinations",
                column: "StartLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations",
                column: "ProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Destinations_Customers_CustomerID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Locations_EndLocationID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Locations_StartLocationID",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "FuelItems");

            migrationBuilder.DropTable(
                name: "Kilometre");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Commodity");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Customers_CustomerID",
                table: "Destinations",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Locations_EndLocationID",
                table: "Destinations",
                column: "EndLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Locations_StartLocationID",
                table: "Destinations",
                column: "StartLocationID",
                principalTable: "Locations",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations",
                column: "ProvinceID",
                principalTable: "Provinces",
                principalColumn: "ProvinceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "CountryID",
                onDelete: ReferentialAction.Cascade);

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
