using System.Net.Mime;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace Deopeia.Common.Bff.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Authorize]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public abstract class ApiController : ControllerBase { }
