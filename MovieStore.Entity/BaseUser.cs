using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Entity
{
    public class BaseUser : IdentityUser, IEntity
    {
        public Cast Cast { get; set; }
        public Director Director { get; set; }
        public Customer Customer { get; set; }
    }
}
