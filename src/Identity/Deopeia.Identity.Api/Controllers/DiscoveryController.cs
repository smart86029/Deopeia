using Deopeia.Identity.Application.Tokens;
using Deopeia.Identity.Application.WellKnown.GetConfiguration;

namespace Deopeia.Identity.Api.Controllers;

[Route(".well-known")]
[EnableCors(CorsPolicies.Oidc)]
public class DiscoveryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    [HttpGet("openid-configuration")]
    public async Task<IActionResult> GetConfiguration()
    {
        var query = new GetConfigurationQuery();
        var configuration = await _mediator.Send(query);
        return new JsonResult(configuration, JsonSerializerOptions);
    }

    [HttpGet("jwks.json")]
    public IActionResult GetJsonWebKeys([FromServices] ITokenService tokenService)
    {
        var jsonWebKey = tokenService.JsonWebKey;
        return new JsonResult(
            new { Keys = new List<JsonWebKeyDto> { jsonWebKey } },
            JsonSerializerOptions
        );
    }
}
