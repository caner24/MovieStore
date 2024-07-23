using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data.EfCore.Concrete;
using MovieStore.Data.Abstract;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Concrete
{
    public class CastDal : EfRepositoryBase<MovieStoreContext, Cast>, ICastDal
    {
        private readonly UserManager<Cast> _userManager;
        public CastDal(MovieStoreContext tContext, UserManager<Cast> userManager) : base(tContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddCast(Cast cast)
        {
            var director = await _userManager.CreateAsync(cast);
            await _userManager.AddToRoleAsync(cast, "Cast");
            return director;
        }

        public async Task<Cast> GetCastByEmail(string email)
        {
            var cast = await _userManager.FindByEmailAsync(email);
            return cast;
        }
    }
}
