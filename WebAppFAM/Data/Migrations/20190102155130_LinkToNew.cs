using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppFAM.Data.Migrations
{
    public partial class LinkToNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

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
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AccountNo = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    SecondName = table.Column<string>(nullable: true),
                    CellNumber = table.Column<string>(maxLength: 15, nullable: false),
                    PDPExpiryDate = table.Column<DateTime>(nullable: false),
                    MedicalExpiryDate = table.Column<DateTime>(nullable: false),
                    PassportNo = table.Column<string>(nullable: true),
                    IDNumber = table.Column<string>(maxLength: 20, nullable: true),
                    NextofKin = table.Column<string>(nullable: true),
                    NextofKinDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "SubContractor",
                columns: table => new
                {
                    SubContractorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AccountNo = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContractor", x => x.SubContractorID);
                });

            migrationBuilder.CreateTable(
                name: "TrailerTypes",
                columns: table => new
                {
                    TrailerTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrailerTypes", x => x.TrailerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryID = table.Column<int>(nullable: false),
                    ProvinceName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ProvinceID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    VinNo = table.Column<string>(maxLength: 20, nullable: false),
                    LicenseExpiry = table.Column<DateTime>(nullable: false),
                    InsuranceExpiry = table.Column<DateTime>(nullable: false),
                    FleetNo = table.Column<string>(maxLength: 6, nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    PhoneNo = table.Column<string>(maxLength: 10, nullable: true),
                    GPSUnitNo = table.Column<string>(maxLength: 20, nullable: true),
                    TrailerTypeID = table.Column<int>(nullable: true),
                    LinkRegistrationNumber = table.Column<string>(maxLength: 10, nullable: true),
                    LinkVinNo = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleID);
                    table.ForeignKey(
                        name: "FK_Vehicles_TrailerTypes_TrailerTypeID",
                        column: x => x.TrailerTypeID,
                        principalTable: "TrailerTypes",
                        principalColumn: "TrailerTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocationName = table.Column<string>(maxLength: 50, nullable: false),
                    GPSCoordinates = table.Column<double>(nullable: false),
                    ProvinceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Locations_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Provinces",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StartLocationID = table.Column<int>(nullable: false),
                    EndLocationID = table.Column<int>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    CustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationID);
                    table.ForeignKey(
                        name: "FK_Destinations_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destinations_Locations_EndLocationID",
                        column: x => x.EndLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destinations_Locations_StartLocationID",
                        column: x => x.StartLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
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
                    UserID = table.Column<string>(nullable: true),
                    HorseID = table.Column<int>(nullable: true),
                    TrailerID = table.Column<int>(nullable: true),
                    CommodityID = table.Column<int>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    SubContractorID = table.Column<int>(nullable: true),
                    ExpectedCollectionDateTime = table.Column<DateTime>(nullable: false),
                    ActualCollectionDateTime = table.Column<DateTime>(nullable: false),
                    DiffCollectionTimeHrs = table.Column<double>(nullable: false),
                    DiffStartTimeHrs = table.Column<double>(nullable: false),
                    DiffEndTimeHrs = table.Column<double>(nullable: false),
                    ExpectedStartDateTime = table.Column<DateTime>(nullable: false),
                    ActualStartDateTime = table.Column<DateTime>(nullable: false),
                    ExpectedCompletionDateTime = table.Column<DateTime>(nullable: false),
                    ActualCompletionDateTime = table.Column<DateTime>(nullable: false),
                    InvoiceNo = table.Column<string>(nullable: false),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    CustomerReferenceNo = table.Column<string>(nullable: false),
                    InvoicedKms = table.Column<double>(nullable: false),
                    InvoiceRate = table.Column<double>(nullable: false),
                    InvoiceAmount = table.Column<double>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Trips_Status_StatusID",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_SubContractor_SubContractorID",
                        column: x => x.SubContractorID,
                        principalTable: "SubContractor",
                        principalColumn: "SubContractorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_TrailerID",
                        column: x => x.TrailerID,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuelItems",
                columns: table => new
                {
                    FuelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripID = table.Column<int>(nullable: false),
                    PurchaseOrderID = table.Column<string>(nullable: false),
                    FuelRate = table.Column<double>(nullable: false),
                    Litres = table.Column<double>(nullable: false),
                    Odometre = table.Column<double>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelItems", x => x.FuelID);
                    table.ForeignKey(
                        name: "FK_FuelItems_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripFiles",
                columns: table => new
                {
                    TripFileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TripID = table.Column<int>(nullable: false),
                    TripFileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileDateTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripFiles", x => x.TripFileID);
                    table.ForeignKey(
                        name: "FK_TripFiles_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_CustomerID",
                table: "Destinations",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_EndLocationID",
                table: "Destinations",
                column: "EndLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_StartLocationID",
                table: "Destinations",
                column: "StartLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FuelItems_TripID",
                table: "FuelItems",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Kilometre_TripID",
                table: "Kilometre",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ProvinceID",
                table: "Locations",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID",
                table: "Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_TripFiles_TripID",
                table: "TripFiles",
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
                name: "IX_Trips_StatusID",
                table: "Trips",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SubContractorID",
                table: "Trips",
                column: "SubContractorID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TrailerID",
                table: "Trips",
                column: "TrailerID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserID",
                table: "Trips",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TrailerTypeID",
                table: "Vehicles",
                column: "TrailerTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FuelItems");

            migrationBuilder.DropTable(
                name: "Kilometre");

            migrationBuilder.DropTable(
                name: "TripFiles");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Commodity");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "SubContractor");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "TrailerTypes");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
