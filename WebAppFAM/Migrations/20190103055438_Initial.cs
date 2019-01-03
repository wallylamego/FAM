using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Migrations
{
    public partial class Initial : Migration
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
                name: "FK_FuelItems_Trips_TripID",
                table: "FuelItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Kilometre_Trips_TripID",
                table: "Kilometre");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_TripFiles_Trips_TripID",
                table: "TripFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Destinations_DestinationID",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

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
                name: "FK_FuelItems_Trips_TripID",
                table: "FuelItems",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kilometre_Trips_TripID",
                table: "Kilometre",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
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
                name: "FK_TripFiles_Trips_TripID",
                table: "TripFiles",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Destinations_DestinationID",
                table: "Trips",
                column: "DestinationID",
                principalTable: "Destinations",
                principalColumn: "DestinationID",
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
                name: "FK_FuelItems_Trips_TripID",
                table: "FuelItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Kilometre_Trips_TripID",
                table: "Kilometre");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Provinces_ProvinceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Provinces_Countries_CountryID",
                table: "Provinces");

            migrationBuilder.DropForeignKey(
                name: "FK_TripFiles_Trips_TripID",
                table: "TripFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Destinations_DestinationID",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                table: "Vehicles");

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
                name: "FK_FuelItems_Trips_TripID",
                table: "FuelItems",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kilometre_Trips_TripID",
                table: "Kilometre",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
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
                name: "FK_TripFiles_Trips_TripID",
                table: "TripFiles",
                column: "TripID",
                principalTable: "Trips",
                principalColumn: "TripID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Destinations_DestinationID",
                table: "Trips",
                column: "DestinationID",
                principalTable: "Destinations",
                principalColumn: "DestinationID",
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
