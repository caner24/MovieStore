using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;

namespace MovieStore.Entity
{
    public class Customer : IdentityUser, IEntity
    {
        public Customer()
        {
            Movies = new HashSet<Movie>();
            FavoriteKind = new HashSet<Kind>();
        }
        public HashSet<Movie> Movies { get; set; }

        public HashSet<Kind> FavoriteKind { get; set; }
    }
}
