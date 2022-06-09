namespace VacationManager.API.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using System.Net;
    using VacationManager.Core.Constants;
    using VacationManager.Domain.Models;

    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var exceptionMessage = contextFeature.Error.Message;

                    var exceptionDetails = new ExceptionDetails();

                    if (contextFeature != null)
                    {
                        exceptionDetails.Message = exceptionMessage;

                        switch (exceptionMessage)
                        {
                            case ExceptionMessages.IncorrectEmailOrPassword:
                                context.Response.StatusCode =
                                exceptionDetails.StatusCode = (int)HttpStatusCode.Conflict;
                                break;

                            case ExceptionMessages.NotConfirmedRegistration:
                                context.Response.StatusCode =
                                exceptionDetails.StatusCode = (int)HttpStatusCode.Conflict;
                                break;
                        }

                        await context.Response.WriteAsync(exceptionDetails.ToString());
                    }
                });
            });
        }
    }
}
