using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class Cast : IEntity
    {
        public Cast()
        {
            Movies = new HashSet<Movie>();
        }

        public string? BaseUserId { get; set; }
        public BaseUser BaseUser { get; set; }
        public HashSet<Movie> Movies { get; set; }

    }
}
