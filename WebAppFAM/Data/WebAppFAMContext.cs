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
            modelbuilder.Entity("WebAppFAM.Data.ApplicationUser", b =>
            {
                b.Property<string>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp");

                b.Property<string>("Email");

                b.Property<bool>("EmailConfirmed");

                b.Property<bool>("LockoutEnabled");

                b.Property<System.DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail");

                b.Property<string>("NormalizedUserName");

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName");

                b.HasKey("Id");

                b.ToTable("AspNetUsers");
            });
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
