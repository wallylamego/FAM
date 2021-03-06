﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppFAM.Data;

namespace WebAppFAM.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebAppFAM.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("WebAppFAM.Models.Commodity", b =>
                {
                    b.Property<int>("CommodityID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CommodityID");

                    b.ToTable("Commodity");
                });

            modelBuilder.Entity("WebAppFAM.Models.Country", b =>
                {
                    b.Property<int>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("CountryID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WebAppFAM.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedUtc")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WebAppFAM.Models.Destination", b =>
                {
                    b.Property<int>("DestinationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerID");

                    b.Property<double>("Distance");

                    b.Property<int>("EndLocationID");

                    b.Property<int>("StartLocationID");

                    b.HasKey("DestinationID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("EndLocationID");

                    b.HasIndex("StartLocationID");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("WebAppFAM.Models.Driver", b =>
                {
                    b.Property<int>("DriverID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellNumber")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("IDNumber")
                        .HasMaxLength(20);

                    b.Property<DateTime>("MedicalExpiryDate");

                    b.Property<string>("NextofKin");

                    b.Property<DateTime>("NextofKinDate");

                    b.Property<DateTime>("PDPExpiryDate");

                    b.Property<string>("PassportNo");

                    b.Property<string>("SecondName");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("DriverID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("WebAppFAM.Models.Fuel", b =>
                {
                    b.Property<int>("FuelID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<double>("FuelRate");

                    b.Property<double>("Litres");

                    b.Property<double>("Odometre");

                    b.Property<string>("PurchaseOrderID")
                        .IsRequired();

                    b.Property<int>("TripID");

                    b.HasKey("FuelID");

                    b.HasIndex("TripID");

                    b.ToTable("FuelItems");
                });

            modelBuilder.Entity("WebAppFAM.Models.Kilometre", b =>
                {
                    b.Property<int>("KilometreID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<double>("KmsTravelled");

                    b.Property<int>("TripID");

                    b.HasKey("KilometreID");

                    b.HasIndex("TripID");

                    b.ToTable("Kilometre");
                });

            modelBuilder.Entity("WebAppFAM.Models.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("GPSCoordinates");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProvinceID");

                    b.HasKey("LocationID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("WebAppFAM.Models.Province", b =>
                {
                    b.Property<int>("ProvinceID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryID");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("ProvinceID");

                    b.HasIndex("CountryID");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("WebAppFAM.Models.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("WebAppFAM.Models.SubContractor", b =>
                {
                    b.Property<int>("SubContractorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedUtc")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("SubContractorID");

                    b.ToTable("SubContractor");
                });

            modelBuilder.Entity("WebAppFAM.Models.TrailerType", b =>
                {
                    b.Property<int>("TrailerTypeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.HasKey("TrailerTypeID");

                    b.ToTable("TrailerTypes");
                });

            modelBuilder.Entity("WebAppFAM.Models.Trip", b =>
                {
                    b.Property<int>("TripID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActualCollectionDateTime");

                    b.Property<DateTime>("ActualCompletionDateTime");

                    b.Property<DateTime>("ActualStartDateTime");

                    b.Property<int?>("CommodityID");

                    b.Property<DateTime>("CreatedUtc")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("CustomerReferenceNo")
                        .IsRequired();

                    b.Property<int>("DestinationID");

                    b.Property<double>("DiffCollectionTimeHrs");

                    b.Property<double>("DiffEndTimeHrs");

                    b.Property<double>("DiffStartTimeHrs");

                    b.Property<int?>("DriverID");

                    b.Property<DateTime>("ExpectedCollectionDateTime");

                    b.Property<DateTime>("ExpectedCompletionDateTime");

                    b.Property<DateTime>("ExpectedStartDateTime");

                    b.Property<int?>("HorseID");

                    b.Property<double>("InvoiceAmount");

                    b.Property<DateTime>("InvoiceDate");

                    b.Property<string>("InvoiceNo")
                        .IsRequired();

                    b.Property<double>("InvoiceRate");

                    b.Property<double>("InvoicedKms");

                    b.Property<bool>("ReturnTrip");

                    b.Property<int>("ReturnTripID");

                    b.Property<bool>("ReverseDestinationID");

                    b.Property<int?>("StatusID");

                    b.Property<int?>("SubContractorID");

                    b.Property<int?>("TrailerID");

                    b.Property<string>("TripCode")
                        .IsRequired();

                    b.Property<string>("UserID");

                    b.HasKey("TripID");

                    b.HasIndex("CommodityID");

                    b.HasIndex("DestinationID");

                    b.HasIndex("DriverID");

                    b.HasIndex("HorseID");

                    b.HasIndex("StatusID");

                    b.HasIndex("SubContractorID");

                    b.HasIndex("TrailerID");

                    b.HasIndex("UserID");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("WebAppFAM.Models.TripFile", b =>
                {
                    b.Property<int>("TripFileID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileDateTime");

                    b.Property<string>("FilePath");

                    b.Property<string>("TripFileName");

                    b.Property<int>("TripID");

                    b.HasKey("TripFileID");

                    b.HasIndex("TripID");

                    b.ToTable("TripFiles");
                });

            modelBuilder.Entity("WebAppFAM.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FleetNo")
                        .IsRequired()
                        .HasMaxLength(6);

                    b.Property<DateTime>("InsuranceExpiry");

                    b.Property<DateTime>("LicenseExpiry");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("VinNo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("VehicleID");

                    b.ToTable("Vehicles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Vehicle");
                });

            modelBuilder.Entity("WebAppFAM.Models.Horse", b =>
                {
                    b.HasBaseType("WebAppFAM.Models.Vehicle");

                    b.Property<string>("GPSUnitNo")
                        .HasMaxLength(20);

                    b.Property<string>("PhoneNo")
                        .HasMaxLength(10);

                    b.ToTable("Horse");

                    b.HasDiscriminator().HasValue("Horse");
                });

            modelBuilder.Entity("WebAppFAM.Models.Trailer", b =>
                {
                    b.HasBaseType("WebAppFAM.Models.Vehicle");

                    b.Property<string>("LinkRegistrationNumber")
                        .HasMaxLength(10);

                    b.Property<string>("LinkVinNo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("TrailerTypeID");

                    b.HasIndex("TrailerTypeID");

                    b.ToTable("Trailer");

                    b.HasDiscriminator().HasValue("Trailer");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebAppFAM.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebAppFAM.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebAppFAM.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebAppFAM.Data.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAppFAM.Models.Destination", b =>
                {
                    b.HasOne("WebAppFAM.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Location", "EndLocation")
                        .WithMany()
                        .HasForeignKey("EndLocationID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Location", "StartLocation")
                        .WithMany()
                        .HasForeignKey("StartLocationID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Fuel", b =>
                {
                    b.HasOne("WebAppFAM.Models.Trip", "Trip")
                        .WithMany("FuelItems")
                        .HasForeignKey("TripID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Kilometre", b =>
                {
                    b.HasOne("WebAppFAM.Models.Trip", "Trip")
                        .WithMany("Kilometres")
                        .HasForeignKey("TripID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Location", b =>
                {
                    b.HasOne("WebAppFAM.Models.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Province", b =>
                {
                    b.HasOne("WebAppFAM.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Trip", b =>
                {
                    b.HasOne("WebAppFAM.Models.Commodity", "Commodity")
                        .WithMany()
                        .HasForeignKey("CommodityID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Destination", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Horse", "Horse")
                        .WithMany()
                        .HasForeignKey("HorseID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.SubContractor", "SubContractor")
                        .WithMany()
                        .HasForeignKey("SubContractorID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Models.Trailer", "Trailer")
                        .WithMany()
                        .HasForeignKey("TrailerID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebAppFAM.Data.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.TripFile", b =>
                {
                    b.HasOne("WebAppFAM.Models.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("WebAppFAM.Models.Trailer", b =>
                {
                    b.HasOne("WebAppFAM.Models.TrailerType", "TrailerType")
                        .WithMany()
                        .HasForeignKey("TrailerTypeID")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
