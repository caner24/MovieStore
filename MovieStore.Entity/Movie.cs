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
            Director = new Director();
            Customers = new HashSet<Customer>();
        }
        public int Id { get; set; }
        public string? MovieName { get; set; }
        public DateTime MovieDate { get; set; }
        public HashSet<Kind> Kinds { get; set; }
        public Director Director { get; set; }
        public HashSet<Customer> Customers { get; set; }
        public double Price { get; set; }
    }
}
