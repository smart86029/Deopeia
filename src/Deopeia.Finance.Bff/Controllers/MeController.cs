using Deopeia.Finance.Bff.Models.Identity;

namespace Deopeia.Finance.Bff.Controllers;

public class MeController(IIdentityApi identityApi, HttpClient httpClient) : ApiController
{
    private readonly IIdentityApi _identityApi = identityApi;
    private readonly HttpClient _httpClient = httpClient;

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

    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile()
    {
        var avatar = await _identityApi.GetAvatar(User.GetUserId());
        var result = new { AvatarUrl = avatar };

        return Ok(result);
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
