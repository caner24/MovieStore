using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Concrete
{
    public class MovieStoreContext : IdentityDbContext<IdentityUser>
    {
        public MovieStoreContext(DbContextOptions options) : base(options)
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
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
