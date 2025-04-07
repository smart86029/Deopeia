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

    [HttpGet("Avatar")]
    public async Task<IActionResult> GetAvatar()
    {
        var httpContent = await _identityApi.GetAvatar(User.GetUserId());
        if (httpContent is null)
        {
            return NotFound();
        }

        return File(httpContent.ReadAsStream(), httpContent.Headers.ContentType!.ToString());
    }

    [HttpPut("Avatar")]
    public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var command = new UploadAvatarCommand(file.FileName, memoryStream.ToArray());
        await _identityApi.UploadAvatar(User.GetUserId(), command);

        return NoContent();
    }

    [HttpPut("Password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        await _identityApi.ChangePassword(User.GetUserId(), command);

        return NoContent();
    }
}
