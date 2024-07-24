

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MovieStore.Data.Concrete;
using MovieStore.Entity;
using MovieStore.Extensions;
using Serilog;
using System.Security.Claims;

Log.Logger = new LoggerConfiguration()
      .WriteTo.Console()
      .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddProblemDetails();
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddDbContext<MovieStoreContext>(options => options.UseInMemoryDatabase("MovieStoreDb"));
    builder.Services.IdentityConfiguration(builder.Configuration);
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.ServiceLifetimeOptions();
    builder.Services.AddAutoMapper(typeof(Program));
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddTransient<ClaimsPrincipal>(provider =>
    {
        var context = provider.GetService<IHttpContextAccessor>();
        return context?.HttpContext?.User ?? new ClaimsPrincipal();
    });


    var app = builder.Build();


    #region SeedData

    await using (var scope = app.Services.CreateAsyncScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateAsync(new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" });
        await roleManager.CreateAsync(new IdentityRole { Name = "Cast", NormalizedName = "CAST" });
        await roleManager.CreateAsync(new IdentityRole { Name = "Director", NormalizedName = "DIRECTOR" });

    }
    #endregion
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
        app.UseHsts();

    app.UseExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
