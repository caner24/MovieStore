using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Concrete
{
    public class MovieStoreContext : IdentityDbContext<IdentityUser>
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        {

        }
        public MovieStoreContext()
        {

        }


        public DbSet<Customer> Customer { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>()
             .ToTable("Movie", t => t.IsTemporal());


            builder.Entity<Movie>()
                .HasMany(m => m.Customers)
                .WithMany(c => c.Movies)
                .UsingEntity<Dictionary<string, object>>(
                    "MovieCustomer",
                    j => j
                        .HasOne<Customer>()
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction), 
                    j => j
                        .HasOne<Movie>()
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction),
                    j =>
                    {
                        j.HasKey("CustomerId", "MovieId");
                        j.ToTable("MovieCustomers"); 
                    });

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Director", NormalizedName = "DIRECTOR" },
                    new IdentityRole { Name = "Cast", NormalizedName = "CAST" },
                        new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
                );


            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=MovieStore;Integrated Security=True;TrustServerCertificate=True;Encrypt=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

    }
}
