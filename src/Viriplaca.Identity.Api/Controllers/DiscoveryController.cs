using Viriplaca.Identity.App.WellKnown.GetConfiguration;

namespace Viriplaca.Identity.Api.Controllers;

[AllowAnonymous]
[Route(".well-known")]
[EnableCors(CorsPolicies.Oidc)]
public class DiscoveryController : ApiController<DiscoveryController>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    [HttpGet("openid-configuration")]
    public async Task<IActionResult> GetConfiguration()
    {
        var query = new GetConfigurationQuery();
        var configuration = await Sender.Send(query);

        return new JsonResult(configuration, JsonSerializerOptions);
    }
}
