using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAppFAM.Data;
using WebAppFAM.Models;

namespace WebAppFAM.Models
{
    public class WebAppFAMContext : DbContext
    {
        public WebAppFAMContext (DbContextOptions<WebAppFAMContext> options)
            : base(options)
        {

        }
    protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            //modelbuilder.Entity("WebAppFAM.Data.ApplicationUser", b =>
            //{
            //    b.Property<string>("Id")
            //        .ValueGeneratedOnAdd();

            //    b.Property<int>("AccessFailedCount");

            //    b.Property<string>("ConcurrencyStamp");

            //    b.Property<string>("Email");

            //    b.Property<bool>("EmailConfirmed");

            //    b.Property<bool>("LockoutEnabled");

            //    b.Property<System.DateTimeOffset?>("LockoutEnd");

            //    b.Property<string>("NormalizedEmail");

            //    b.Property<string>("NormalizedUserName");

            //    b.Property<string>("PasswordHash");

            //    b.Property<string>("PhoneNumber");

            //    b.Property<bool>("PhoneNumberConfirmed");

            //    b.Property<string>("SecurityStamp");

            //    b.Property<bool>("TwoFactorEnabled");

            //    b.Property<string>("UserName");

            //    b.HasKey("Id");

            //    b.ToTable("AspNetUsers");
            //});
            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            //{
            //    b.Property<string>("Id");

            //    b.Property<string>("ConcurrencyStamp")
            //        .IsConcurrencyToken();

            //    b.Property<string>("Name")
            //        .HasAnnotation("MaxLength", 256);

            //    b.Property<string>("NormalizedName")
            //        .HasAnnotation("MaxLength", 256);

            //    b.HasKey("Id");

            //    b.HasIndex("NormalizedName")
            //        .HasName("RoleNameIndex");

            //    b.ToTable("AspNetRoles");
            //});
            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            //{
            //    b.Property<int>("Id")
            //        .ValueGeneratedOnAdd();

            //    b.Property<string>("ClaimType");

            //    b.Property<string>("ClaimValue");

            //    b.Property<string>("RoleId")
            //        .IsRequired();

            //    b.HasKey("Id");

            //    b.HasIndex("RoleId");

            //    b.ToTable("AspNetRoleClaims");
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            //{
            //    b.Property<int>("Id")
            //        .ValueGeneratedOnAdd();

            //    b.Property<string>("ClaimType");

            //    b.Property<string>("ClaimValue");

            //    b.Property<string>("UserId")
            //        .IsRequired();

            //    b.HasKey("Id");

            //    b.HasIndex("UserId");

            //    b.ToTable("AspNetUserClaims");
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            //{
            //    b.Property<string>("LoginProvider");

            //    b.Property<string>("ProviderKey");

            //    b.Property<string>("ProviderDisplayName");

            //    b.Property<string>("UserId")
            //        .IsRequired();

            //    b.HasKey("LoginProvider", "ProviderKey");

            //    b.HasIndex("UserId");

            //    b.ToTable("AspNetUserLogins");
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            //{
            //    b.Property<string>("UserId");

            //    b.Property<string>("RoleId");

            //    b.HasKey("UserId", "RoleId");

            //    b.HasIndex("RoleId");

            //    b.HasIndex("UserId");

            //    b.ToTable("AspNetUserRoles");
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            //{
            //    b.Property<string>("UserId");

            //    b.Property<string>("LoginProvider");

            //    b.Property<string>("Name");

            //    b.Property<string>("Value");

            //    b.HasKey("UserId", "LoginProvider", "Name");

            //    b.ToTable("AspNetUserTokens");
            //});
            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            //{
            //    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
            //        .WithMany("Claims")
            //        .HasForeignKey("RoleId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            //{
            //    b.HasOne("WebAppFAM.Model.ApplicationUser")
            //        .WithMany("Claims")
            //        .HasForeignKey("UserId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            //{
            //    b.HasOne("WebAppFAM.Model.ApplicationUser")
            //        .WithMany("Logins")
            //        .HasForeignKey("UserId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

            //modelbuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            //{
            //    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
            //        .WithMany("Users")
            //        .HasForeignKey("RoleId")
            //        .OnDelete(DeleteBehavior.Cascade);

            //    b.HasOne("WebAppFAM.Model.ApplicationUser")
            //        .WithMany("Roles")
            //        .HasForeignKey("UserId")
            //        .OnDelete(DeleteBehavior.Cascade);
            //});
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelbuilder);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<TrailerType> TrailerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Fuel> FuelItems { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripFile> TripFiles { get; set; }
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Commodity> Commodity { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<SubContractor> SubContractor { get; set; }
    }
}
