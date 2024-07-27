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
    public class MovieStoreContext : IdentityDbContext<BaseUser>
    {
        public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
        {

        }
        public MovieStoreContext()
        {

        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<MovieCustomer> MovieCustomer { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Kind> Kind { get; set; }
        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>()
     .ToTable("Movie", t => t.IsTemporal());

            builder.Entity<MovieCustomer>()
.ToTable("MovieCustomer", t => t.IsTemporal());

            builder.Entity<MovieCustomer>().HasKey(x => new { x.CustomerId, x.MovieId });

            builder.Entity<MovieCustomer>()
           .HasOne(x => x.Movie)
           .WithMany(x => x.Customers)
           .HasForeignKey(x => x.MovieId)
           .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<MovieCustomer>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Cast>().HasKey(x => x.BaseUserId);
            builder.Entity<Cast>().HasOne(x => x.BaseUser).WithOne(x => x.Cast).HasForeignKey<Cast>(x => x.BaseUserId);

            builder.Entity<Director>().HasKey(x => x.BaseUserId);
            builder.Entity<Director>().HasOne(x => x.BaseUser).WithOne(x => x.Director).HasForeignKey<Director>(x => x.BaseUserId);

            builder.Entity<Customer>().HasKey(x => x.BaseUserId);
            builder.Entity<Customer>().HasOne(x => x.BaseUser).WithOne(x => x.Customer).HasForeignKey<Customer>(x => x.BaseUserId);


            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
            base.OnConfiguring(optionsBuilder);
        }

    }
}
