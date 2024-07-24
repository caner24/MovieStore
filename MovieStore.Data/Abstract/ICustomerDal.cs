using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
using MovieStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Data.Abstract
{
    public interface ICustomerDal : IEntityRepositoryBase<Customer>
    {
        Task<IdentityResult> AddCustomer(BaseUser customer,string password);
        Task<SignInResult> SignIn(string email, string password);

    }
}
