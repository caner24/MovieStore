using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<BaseUser> _userManager;
        public CastDal(MovieStoreContext tContext, UserManager<BaseUser> userManager) : base(tContext)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddCast(BaseUser cast, string password)
        {
            var addedCast = await _userManager.CreateAsync(cast, password);
            await _userManager.AddToRoleAsync(cast, "Cast");
            return addedCast;
        }

    }
}
