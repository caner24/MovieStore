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
    public class CastDal : EfRepositoryBase<MovieStoreContext, Cast>, ICastDal
    {
        public CastDal(MovieStoreContext tContext) : base(tContext)
        {
        }
    }
}
