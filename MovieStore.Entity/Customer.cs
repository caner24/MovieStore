using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;

namespace MovieStore.Entity
{
    public class Customer: IEntity
    {
        public Customer()
        {
            Movies = new HashSet<Movie>();
            FavoriteKind = new HashSet<Kind>();
        }
        public string? BaseUserId { get; set; }
        public BaseUser BaseUser { get; set; }
        public HashSet<Movie> Movies { get; set; }
        public HashSet<Kind> FavoriteKind { get; set; }
    }
}
