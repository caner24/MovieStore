using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity.Dto
{
    public record CreateMovieDto
    {
        public string? MovieName { get; init; }

        public string? DirectorId { get; init; }

        public double Price { get; init; }



    }
}
