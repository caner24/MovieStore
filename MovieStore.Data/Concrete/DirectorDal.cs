using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data.EfCore.Concrete;
using MovieStore.Data.Abstract;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Concrete
{
    public class DirectorDal : EfRepositoryBase<MovieStoreContext, Director>, IDirectorDal
    {
        private readonly UserManager<Director> _userManager;
        public DirectorDal(MovieStoreContext tContext, UserManager<Director> userManager) : base(tContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddDirector(Director director)
        {
            var userManager = await _userManager.CreateAsync(director);
            await _userManager.AddToRoleAsync(director, "Director");
            return userManager;
        }

        public async Task<Director> GetDirectorByEmail(string email)
        {
            var director = await _userManager.FindByEmailAsync(email);
            return director;
        }
    }
}
