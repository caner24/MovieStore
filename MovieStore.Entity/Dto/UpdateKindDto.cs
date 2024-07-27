using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity.Dto
{
    public record UpdateKindDto
    {
        public int Id { get; init; }

        public string? KindName { get; init; }
    }
}
