using Application.Interfaces;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Presentation.Extensions
{
  public static class ExceptionMiddlewareExtensions
  {
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
      app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
  }
}
