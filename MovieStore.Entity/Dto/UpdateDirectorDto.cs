using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity.Dto
{
    public record UpdateDirectorDto : BaseUserForCreationDto
    {
        public UpdateDirectorDto()
        {
            Movies = new HashSet<Movie>();
        }
        public HashSet<Movie> Movies { get; init; }
    }
}
