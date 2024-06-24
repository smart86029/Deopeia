using Deopeia.Common.Localization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Deopeia.Common.Api;

internal class ExceptionHandler(
    ILogger<ExceptionHandler> logger,
    IStringLocalizer localizer,
    IProblemDetailsService problemDetailsService,
    IWebHostEnvironment environment
) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger = logger;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
    private readonly IWebHostEnvironment _environment = environment;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = _environment.IsDevelopment()
            ? exception.ToString()
            : _localizer.GetErrorString("SystemException");

        switch (exception)
        {
            //case DomainException domainException:
            //    statusCode = StatusCodes.Status400BadRequest;
            //    message = LocalizeMessage(domainException);
            //    break;

            //case AccessDeniedException accessDeniedException:
            //    statusCode = StatusCodes.Status403Forbidden;
            //    message = LocalizeMessage(accessDeniedException);
            //    break;

            default:
                _logger.LogError(exception, "Global handle exception.");
                break;
        }

        httpContext.Response.StatusCode = statusCode;
        var result = await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = { Title = message, },
                Exception = exception,
            }
        );

        return result;
    }

    private string LocalizeMessage(LocalizableMessageException exception)
    {
        var localizeStrings = exception
            .Messages.Select(message => _localizer.GetErrorString(message.Code, message.Argument))
            .ToList();

        return string.Join(Environment.NewLine, localizeStrings);
    }
}
