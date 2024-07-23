using Microsoft.AspNetCore.Identity;
using MovieStore.Core.Data;
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
    public class CustomerDal : EfRepositoryBase<MovieStoreContext, Customer>, ICustomerDal
    {
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;
        public CustomerDal(MovieStoreContext tContext, UserManager<Customer> userManager, SignInManager<Customer> signInManager) : base(tContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddCustomer(Customer customer)
        {
            var userManager = await _userManager.CreateAsync(customer);
            await _userManager.AddToRoleAsync(customer, "Customer");
            return userManager;
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            var customer = await _userManager.FindByEmailAsync(email);
            return customer;
        }

        public async Task<SignInResult> SignIn(string email, string password)
        {
            var customer = await _userManager.FindByEmailAsync(email);
            if (customer is null)
                throw new Exception("Customer not found");

            var signInResult = await _signInManager.PasswordSignInAsync(customer, password, true, true);
            return signInResult;
        }
    }
}
