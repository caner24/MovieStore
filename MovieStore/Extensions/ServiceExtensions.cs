using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.Data.Abstract;
using MovieStore.Data.Concrete;
using MovieStore.Entity;

namespace MovieStore.Extensions
{
    public static class ServiceExtensions
    {

        public static void IdentityConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<MovieStoreContext>(options => options.UseInMemoryDatabase("MovieStore"));
            services.AddIdentityApiEndpoints<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
             .AddRoles<IdentityRole>()
             .AddRoleManager<RoleManager<IdentityRole>>()
             .AddUserManager<UserManager<IdentityUser>>()
             .AddEntityFrameworkStores<MovieStoreContext>()
             .AddDefaultTokenProviders();

            // Configure Data Protection Token Provider
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(2));



        }


        public static void ServiceLifetimeOptions(this IServiceCollection services)
        {
            services.AddScoped<ICastDal, CastDal>();
            services.AddScoped<ICustomerDal, CustomerDal>();
            services.AddScoped<IDirectorDal, DirectorDal>();
            services.AddScoped<IMovieDal, MovieDal>();
            services.AddScoped<IKindDal, KindDal>();

            services.AddScoped(provider => new Lazy<ICastDal>(() => provider.GetRequiredService<ICastDal>()));
            services.AddScoped(provider => new Lazy<ICustomerDal>(() => provider.GetRequiredService<ICustomerDal>()));
            services.AddScoped(provider => new Lazy<IDirectorDal>(() => provider.GetRequiredService<IDirectorDal>()));
            services.AddScoped(provider => new Lazy<IMovieDal>(() => provider.GetRequiredService<IMovieDal>()));
            services.AddScoped(provider => new Lazy<IKindDal>(() => provider.GetRequiredService<IKindDal>()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();



        }
    }
}
