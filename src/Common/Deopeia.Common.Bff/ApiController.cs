using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deopeia.Common.Bff;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Authorize]
// [Produces(MediaTypeNames.Application.Json)]
public abstract class ApiController : ControllerBase { }
