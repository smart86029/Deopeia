using Deopeia.AdminPortal.Bff.Models.Me;

namespace Deopeia.AdminPortal.Bff.Controllers;

public class MeController(UserService.UserServiceClient client) : ApiController
{
    private readonly UserService.UserServiceClient _client = client;

    [HttpGet("2fa")]
    public async Task<ActionResult<TwoFactorAuthenticationResponse>> GetTwoFactorAuthentication()
    {
        var grpcRequest = new GetAuthenticatorRequest { UserId = User.GetUserId() };
        var grpcResponse = await _client.GetAuthenticatorAsync(grpcRequest);
        var response = grpcResponse.Adapt<TwoFactorAuthenticationResponse>();
        return Ok(response);
    }

    [HttpPost("2fa")]
    public async Task<IActionResult> EnableTwoFactorAuthentication(
        [FromBody] EnableTwoFactorAuthenticationRequest request
    )
    {
        var grpcRequest = new EnableAuthenticatorRequest
        {
            UserId = User.GetUserId(),
            VerificationCode = request.VerificationCode,
        };
        await _client.EnableAuthenticatorAsync(grpcRequest);
        return NoContent();
    }

    // [HttpGet("Profile")]
    // public async Task<IActionResult> GetProfile()
    // {
    //     var response = await _client.GetAvatar(User.GetUserId());
    //     var result = new { AvatarUrl = response.Content };

    //     return Ok(result);
    // }

    // [HttpPut("Avatar")]
    // public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
    // {
    //     using var memoryStream = new MemoryStream();
    //     await file.CopyToAsync(memoryStream);
    //     var command = new UploadAvatarCommand(file.FileName, memoryStream.ToArray());
    //     await _identityApi.UploadAvatar(User.GetUserId(), command);

    //     return NoContent();
    // }

    // [HttpPut("Password")]
    // public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    // {
    //     await _identityApi.ChangePassword(User.GetUserId(), command);

    //     return NoContent();
    // }
}
