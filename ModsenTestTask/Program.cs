using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using ModsenTestTask.Extensions;
using NLog;

string? pathDirectoryName =Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
_ = LogManager.Setup().LoadConfigurationFromFile(string.Concat(pathDirectoryName, "/Nlog.config"));
var logger = LogManager.GetCurrentClassLogger();
logger.Info("Запуск программы");

try
{
  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.

  builder.Services.ConfigureCors();
  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  var app = builder.Build();

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

  app.UseStaticFiles();

  app.UseCors("CorsPolicy");

  app.UseForwardedHeaders(new ForwardedHeadersOptions
  {
    ForwardedHeaders = ForwardedHeaders.All
  });

  app.UseRouting();

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
