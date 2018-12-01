﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using WebAppFAM.Models;

namespace WebAppFAM.Migrations
{
    [DbContext(typeof(WebAppFAMContext))]
    [Migration("20180710103355_TrailerTypesTake6")]
    partial class TrailerTypesTake6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAppFAM.Models.Driver", b =>
                {
                    b.Property<int>("DriverID")
                        .ValueGeneratedOnAdd();

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

                    b.Property<DateTime>("PassportNo");

                    b.Property<string>("SecondName");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("DriverID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("WebAppFAM.Models.TrailerType", b =>
                {
                    b.Property<int>("TrailerTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(40);

                    b.HasKey("TrailerTypeID");

                    b.ToTable("TrailerTypes");
                });

            modelBuilder.Entity("WebAppFAM.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleID")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("WebAppFAM.Models.Trailer", b =>
                {
                    b.HasOne("WebAppFAM.Models.TrailerType", "TrailerType")
                        .WithMany()
                        .HasForeignKey("TrailerTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
