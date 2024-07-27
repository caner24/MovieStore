using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class Movie : IEntity
    {
        public Movie()
        {
            Kinds = new HashSet<Kind>();
            Customers = new HashSet<MovieCustomer>();
        }
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public DateTime MovieDate { get; set; } = DateTime.Now;
        public HashSet<Kind> Kinds { get; set; }
        public string DirectorId { get; set; }
        public Director Director { get; set; }
        public HashSet<MovieCustomer> Customers { get; set; }
        public double Price { get; set; }
    }
}
