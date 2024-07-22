using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class Cast : IdentityUser, IEntity
    {
        public Cast()
        {
            Movies = new HashSet<Movie>();
        }
        public HashSet<Movie> Movies { get; set; }

    }
}
