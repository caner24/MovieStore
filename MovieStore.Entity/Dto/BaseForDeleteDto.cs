using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity.Dto
{
    public abstract record BaseForDeleteDto
    {
        public string? Id { get; init; }
    }
}
