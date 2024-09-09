using Application.Interfaces;
using Domain.Entities.ErrorModel;
using System.Net;

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
      await context.Response.WriteAsync(new ErrorDetails()
      {
        StatusCode = context.Response.StatusCode,
        Message = ex.Message,
      }.ToString());
    }
  }
}
