using MovieStore.Core.Data;
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
    public class CustomerDal : EfRepositoryBase<MovieStoreContext, Customer>, ICustomerDal
    {
        public CustomerDal(MovieStoreContext tContext) : base(tContext)
        {
        }
    }
}
