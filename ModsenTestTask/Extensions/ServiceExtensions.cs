using Contracts;
using Entities;
using LoggerService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;

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
      .AllowAnyHeader());
    });

    public static void ConfigureLoggerService(this IServiceCollection services) =>
                            services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
    services.AddDbContext<RepositoryContext>(opts =>
    opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
      services.AddScoped<IRepositoryManager, RepositoryManager>();


  }
}
