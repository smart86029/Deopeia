using Deopeia.Finance.Bff.Models.Identity;

namespace Deopeia.Finance.Bff.Controllers;

public class MeController(IIdentityApi identityApi) : ApiController
{
    private readonly IIdentityApi _identityApi = identityApi;

    [HttpGet("Authenticator")]
    public async Task<IActionResult> GetAuthenticator()
    {
        var authenticator = await _identityApi.GetContractsAsync(User.GetUserId());

        return Ok(authenticator);
    }
}
