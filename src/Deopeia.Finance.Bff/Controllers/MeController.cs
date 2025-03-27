using Deopeia.Finance.Bff.Models.Identity;

namespace Deopeia.Finance.Bff.Controllers;

public class MeController(IIdentityApi identityApi) : ApiController
{
    private readonly IIdentityApi _identityApi = identityApi;

    [HttpGet("Authenticator")]
    public async Task<IActionResult> GetAuthenticator()
    {
        var authenticator = await _identityApi.GetAuthenticatorAsync(User.GetUserId());

        return Ok(authenticator);
    }

    [HttpPut("Authenticator")]
    public async Task<IActionResult> EnableAuthenticator(
        [FromBody] EnableAuthenticatorCommand command
    )
    {
        await _identityApi.EnableAuthenticator(User.GetUserId(), command);

        return NoContent();
    }

    [HttpPut("Password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await _identityApi.ChangePassword(User.GetUserId(), command);

        return NoContent();
    }
}
