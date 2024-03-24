using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Product.Entity.ErrorModel;
using Product.Entity.Exceptions;
using Product.Interfaces;

namespace ProductWebApi.Extensions;

public static class ExceptionMiddlewearExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
    {
         //Middleware
        app.UseExceptionHandler(appErr =>
        {
            appErr.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}