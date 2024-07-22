using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class Kind: IEntity
    {
        public Kind()
        {
            Movies = new HashSet<Movie>();
            Customers = new HashSet<Customer>();
        }
        public int Id { get; set; }
        public string? KindName { get; set; }
        public HashSet<Movie> Movies { get; set; }
        public HashSet<Customer> Customers { get; set; }


    }
}
