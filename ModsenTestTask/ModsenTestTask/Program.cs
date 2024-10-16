using Application.Interfaces;
using Application.Mapping;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Presentation.Extensions;
using System.Reflection;

string? pathDirectoryName =Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
_ = LogManager.Setup().LoadConfigurationFromFile(string.Concat(pathDirectoryName, "/Nlog.config"));
var logger = LogManager.GetCurrentClassLogger();
logger.Info("������ ���������");

try
{
  var builder = WebApplication.CreateBuilder(args);
  builder.Configuration.AddJsonFile(pathDirectoryName + "//appsettings.json", optional: false, reloadOnChange: true);

  // Add services to the container.

  builder.Services.ConfigureCors();
  builder.Services.ConfigureLoggerService();
  builder.Services.ConfigureSqlContext(builder.Configuration);
  builder.Services.ConfigureRepositoryManager();
  builder.Services.AddAutoMapper(typeof(Program));
  builder.Services.ConfigureServiceManager();
  builder.Services.AddAuthentication();
  builder.Services.ConfigureIdentity();
  builder.Services.ConfigureJWT(builder.Configuration);
  builder.Services.ConfigureValidator();

 
  builder.Services.AddSingleton<MapsterUserBookConfig>();

  //builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly));

  builder.Services.AddControllers();
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen();

  // ���� ����� ������ �������� ���������� ������� �� ��������� ������ IExceptionHandler
  //builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

  var app = builder.Build();
  var _logger = app.Services.GetRequiredService<ILoggerManager>();

  // ���������� ������ � ������� RequstDelegat
  app.UseExceptionHandlerMiddleware();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    //app.UseDeveloperExceptionPage();
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

  app.UseAuthentication();

  app.UseAuthorization();

  app.MapControllers();

  app.Run();
}
catch(Exception ex) 
{
  logger.Error(ex, "��������� ��������� ��-�� ����������.");
  throw;
}
finally 
{
  NLog.LogManager.Shutdown();
}
