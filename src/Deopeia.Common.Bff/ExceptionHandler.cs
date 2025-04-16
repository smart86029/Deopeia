using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Deopeia.Common.Bff;

internal class ExceptionHandler(
    ILogger<ExceptionHandler> logger,
    IProblemDetailsService problemDetailsService,
    IWebHostEnvironment environment
) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger = logger;
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;
    private readonly IWebHostEnvironment _environment = environment;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        var statusCode = StatusCodes.Status500InternalServerError;
        var message = exception.ToString();

        switch (exception)
        {
            //case AccessDeniedException accessDeniedException:
            //    statusCode = StatusCodes.Status403Forbidden;
            //    message = LocalizeMessage(accessDeniedException);
            //    break;

            case ValidationApiException validationApiException
                when validationApiException.StatusCode == HttpStatusCode.BadRequest:
                statusCode = StatusCodes.Status400BadRequest;
                message = validationApiException.Content?.Title;
                break;

            default:
                _logger.LogError(exception, "Global handle exception.");
                break;
        }

        httpContext.Response.StatusCode = statusCode;
        var result = await _problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = { Title = message },
                Exception = exception,
            }
        );

        return result;
    }
}
