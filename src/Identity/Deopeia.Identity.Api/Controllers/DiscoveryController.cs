using Deopeia.Identity.Application.Tokens;
using Deopeia.Identity.Application.WellKnown.GetConfiguration;

namespace Deopeia.Identity.Api.Controllers;

[Route(".well-known")]
public class DiscoveryController(IMediator mediator) : OidcController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("openid-configuration")]
    public async Task<IActionResult> GetConfiguration()
    {
        var query = new GetConfigurationQuery();
        var configuration = await _mediator.Send(query);
        return Json(configuration);
    }

    [HttpGet("jwks.json")]
    public IActionResult GetJsonWebKeys([FromServices] ITokenService tokenService)
    {
        var jsonWebKey = tokenService.JsonWebKey;
        return Json(new { Keys = new List<JsonWebKeyDto> { jsonWebKey } });
    }
}
