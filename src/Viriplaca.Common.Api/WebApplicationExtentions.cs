using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Viriplaca.Common.Domain;
using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Api;

public static class WebApplicationExtentions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(
            UnicodeRanges.BasicLatin,
            UnicodeRanges.CjkUnifiedIdeographs),
    };

    public static WebApplication UseExceptionHandler(this WebApplication app)
    {
        var isDevelopment = app.Environment.IsDevelopment();
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            ExceptionHandler = async context =>
            {
                var localizer = context.RequestServices.GetRequiredService<IStringLocalizer>();
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                var statusCode = StatusCodes.Status500InternalServerError;
                var message = isDevelopment
                    ? exception?.ToString() ?? localizer.GetErrorString("SystemException")
                    : localizer.GetErrorString("SystemException");

                switch (exception)
                {
                    case DomainException domainException:
                        statusCode = StatusCodes.Status400BadRequest;
                        message = LocalizeMessage(domainException);
                        break;

                    default:
                        var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>();
                        logger.LogError(exception, "例外處理");
                        break;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var body = new { Message = message };
                await context.Response.WriteAsync(body.ToJson(Options));

                string LocalizeMessage(LocalizedMessageException exception)
                {
                    var localizeStrings = exception.Messages
                        .Select(message => localizer.GetErrorString(message.Code, message.Argument))
                        .ToList();
                    return string.Join(Environment.NewLine, localizeStrings);
                }
            }
        });

        return app;
    }
}
