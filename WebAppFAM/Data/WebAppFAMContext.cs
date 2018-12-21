using System.Linq;
using Microsoft.EntityFrameworkCore;

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
    }
}
