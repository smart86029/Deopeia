using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Viriplaca.Common.Domain;
using Viriplaca.Common.Localization;

namespace Viriplaca.Common.Api;

internal class ExceptionHandler(
    ILogger<ExceptionHandler> logger,
    IStringLocalizer localizer,
    IWebHostEnvironment environment)
    : IExceptionHandler
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Encoder = JavaScriptEncoder.Create(
        UnicodeRanges.BasicLatin,
        UnicodeRanges.CjkUnifiedIdeographs),
    };

    private readonly ILogger<ExceptionHandler> _logger = logger;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IWebHostEnvironment _environment = environment;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = _environment.IsDevelopment()
            ? exception?.ToString() ?? _localizer.GetErrorString("SystemException")
            : _localizer.GetErrorString("SystemException");

        switch (exception)
        {
            case DomainException domainException:
                statusCode = StatusCodes.Status400BadRequest;
                message = LocalizeMessage(domainException);
                break;

            default:
                _logger.LogError(exception, "Global handle exception.");
                break;
        }

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        var body = new { Message = message };
        await httpContext.Response.WriteAsync(body.ToJson(Options), cancellationToken);

        return true;

        string LocalizeMessage(LocalizedMessageException exception)
        {
            var localizeStrings = exception.Messages
                .Select(message => _localizer.GetErrorString(message.Code, message.Argument))
                .ToList();
            return string.Join(Environment.NewLine, localizeStrings);
        }
    }
}
