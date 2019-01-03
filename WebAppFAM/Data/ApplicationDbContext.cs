using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAppFAM.Models;

namespace WebAppFAM.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
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
