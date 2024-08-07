using Contracts;
using Entities;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;
using Contracts.Services;
using Service;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ModsenTestTask.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureCors(this IServiceCollection services) =>
      services.AddCors(options =>
    {
      options.AddPolicy("CorsPolicy", builder =>
      builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
      .WithExposedHeaders("X-Pagination"));
  });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
                            services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    services.AddDbContext<RepositoryContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
      services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
      services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureIdentity(this IServiceCollection services)
    {
      var builder = services.AddIdentity<User, IdentityRole>(o =>
      {
        o.Password.RequireDigit = true;
        o.Password.RequireLowercase = false;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 10;
        o.User.RequireUniqueEmail = true;
      })
      .AddEntityFrameworkStores<RepositoryContext>()
      .AddDefaultTokenProviders();
    }
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtSettings = configuration.GetSection("JwtSettings");
      var secretKey = Environment.GetEnvironmentVariable("SECRET");
      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtSettings["validIssuer"],
          ValidAudience = jwtSettings["validAudience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
      });
    }
  }
}
