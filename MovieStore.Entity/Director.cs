using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class Director : IdentityUser, IEntity
    {
        public Director()
        {
            Movie = new Movie();
        }
        public Movie Movie { get; set; }

    }
}
