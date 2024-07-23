

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MovieStore.Entity;
using MovieStore.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
      .WriteTo.Console()
      .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.IdentityConfiguration(builder.Configuration);
    builder.Services.ServiceLifetimeOptions();
    builder.Services.AddAutoMapper(typeof(Program));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
        app.UseHsts();


    app.MapGroup("api/identity").MapIdentityApi<IdentityUser>();
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
