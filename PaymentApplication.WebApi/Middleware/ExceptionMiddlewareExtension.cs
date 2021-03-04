using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Payment.Interfaces.ILogging;
using Payment.Models.Logging_Models;

namespace PaymentApplication.WebApi.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILog logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        logger.Error(
                            $"Http Request Information:{Environment.NewLine}" +
                            $"Schema:{context.Request.Scheme} {Environment.NewLine}" +
                            $"Host: {context.Request.Host} {Environment.NewLine}" +
                            $"Path: {context.Request.Path} {Environment.NewLine}" +
                            $"QueryString: {context.Request.QueryString} {Environment.NewLine}" +
                            $"Error Message: {contextFeature.Error.Message} {Environment.NewLine}" +
                            $"Inner Exception: {contextFeature.Error.InnerException} {Environment.NewLine}" +
                            $"Error StackTrace: {contextFeature.Error.StackTrace} {Environment.NewLine}" +
                            $"Error Source: {contextFeature.Error.Source} {Environment.NewLine}" +
                            $"Error Detail: {contextFeature.Error} "
);

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please contact the admin!"
                        }.ToString());
                    }
                });
            });
        }
    }
}