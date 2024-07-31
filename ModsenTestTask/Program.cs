using Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using ModsenTestTask.Extensions;
using NLog;
using System.Reflection;

string? pathDirectoryName =Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
_ = LogManager.Setup().LoadConfigurationFromFile(string.Concat(pathDirectoryName, "/Nlog.config"));
var logger = LogManager.GetCurrentClassLogger();
logger.Info("Запуск программы");

try
{
  var builder = WebApplication.CreateBuilder(args);
  builder.Configuration.AddJsonFile(pathDirectoryName + "//appsettings.json", optional: false, reloadOnChange: true);

  // Add services to the container.

  builder.Services.ConfigureCors();
  builder.Services.ConfigureLoggerService();
  builder.Services.ConfigureSqlContext(builder.Configuration);
  builder.Services.ConfigureRepositoryManager();
  builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var app = builder.Build();
  var _logger = app.Services.GetRequiredService<ILoggerManager>();
  app.ConfigureExceptionHandler(_logger);
  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
  }
  else
  {
    app.UseHsts();
  }

  app.UseHttpsRedirection();

  //app.UseStaticFiles();

  app.UseForwardedHeaders(new ForwardedHeadersOptions
  {
    ForwardedHeaders = ForwardedHeaders.All
  });

  app.UseCors("CorsPolicy");

  //app.UseRouting();

  app.UseAuthorization();

  app.MapControllers();

  app.Run();
}
catch(Exception ex) 
{
  logger.Error(ex, "Остановка программы из-за исключения.");
  throw;
}
finally 
{
  NLog.LogManager.Shutdown();
}
