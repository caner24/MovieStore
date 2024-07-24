using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<BaseUser> _userManager;
        public DirectorDal(MovieStoreContext tContext, UserManager<BaseUser> userManager) : base(tContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddDirector(BaseUser director, string password)
        {
            var userManager = await _userManager.CreateAsync(director,password);
            await _userManager.AddToRoleAsync(director, "Director");
            return userManager;
        }

    }
}
