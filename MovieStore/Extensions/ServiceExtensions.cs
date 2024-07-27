using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.ActionFilters;
using MovieStore.Data.Abstract;
using MovieStore.Data.Concrete;
using MovieStore.Entity;
using MovieStore.Entity.Dto;
using MovieStore.Validation;
using System.Reflection;

namespace MovieStore.Extensions
{
    public static class ServiceExtensions
    {

        public static void IdentityConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<BaseUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IValidator<BaseForDeleteDto>, BaseForDeleteDtoValidator>();
            services.AddScoped<IValidator<BaseUserForCreationDto>, BaseUserForCreationDtoValidator>();
            services.AddScoped<IValidator<CreateKindDto>, CreateKindDtoValidator>();
            services.AddScoped<IValidator<UpdateDirectorDto>, UpdateDirectorDtoValidator>();
            services.AddScoped<IValidator<UpdateKindDto>, UpdateKindDtoValidator>();

            services.AddScoped<ValidationFilterAttribute>();




        }
    }
}
