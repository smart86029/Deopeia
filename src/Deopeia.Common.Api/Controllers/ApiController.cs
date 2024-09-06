using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Deopeia.Common.Api.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Authorize]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class ApiController<TController> : ControllerBase
{
    private ILogger<TController>? _logger;
    private ISender? _sender;

    protected ILogger<TController> Logger =>
        _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<TController>>();

    protected ISender Sender =>
        _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
