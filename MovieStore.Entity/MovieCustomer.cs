using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class MovieCustomer
    {
        public string CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
