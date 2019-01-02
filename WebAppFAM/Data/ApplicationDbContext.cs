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
            builder.Entity("WebAppFAM.Models.Customer", b =>
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
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Customer> Customers { get; set; }

    }
}
