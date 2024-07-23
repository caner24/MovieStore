using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity.Dto
{
    public abstract record BaseUserForCreationDto
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
    }
}
