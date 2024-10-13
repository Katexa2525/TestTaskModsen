using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Text.Json;

namespace Presentation.Extensions
{
  public class ExceptionHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILoggerManager _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next.Invoke(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionMessageAsync(context, ex, _logger).ConfigureAwait(false);
      }
    }

    private async Task HandleExceptionMessageAsync(HttpContext context, Exception ex, ILoggerManager logger)
    {
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";
      logger.LogError($"Something went wrong: {ex.StackTrace}");
      await context.Response.WriteAsync(JsonSerializer.Serialize(
        new ProblemDetails()
        {
          Status = context.Response.StatusCode,
          Title = !string.IsNullOrEmpty(ex.Message) ? ex.Message : string.Empty
        }).ToString());
    }
  }
}
