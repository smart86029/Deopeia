using Deopeia.AdminPortal.Bff.Models.Me;
using Google.Protobuf;

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

    [HttpGet("Profile")]
    public async Task<ActionResult<ProfileResponse>> GetProfile()
    {
        var grpcRequest = new GetAvatarRequest { UserId = User.GetUserId() };
        var grpcResponse = await _client.GetAvatarAsync(grpcRequest);
        var result = new ProfileResponse(grpcResponse.Url);
        var a = User;
        return Ok(result);
    }

    [HttpPut("Avatar")]
    public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
    {
        var call = _client.UploadAvatar();
        await call.RequestStream.WriteAsync(
            new UploadAvatarRequest { UserId = User.GetUserId(), FileName = file.FileName }
        );

        var buffer = new byte[32 * 1024];
        using var fileStream = file.OpenReadStream();

        while (true)
        {
            var bytesRead = await fileStream.ReadAsync(buffer);
            if (bytesRead == 0)
            {
                break;
            }

            await call.RequestStream.WriteAsync(
                new UploadAvatarRequest
                {
                    Content = UnsafeByteOperations.UnsafeWrap(buffer.AsMemory(0, bytesRead)),
                }
            );
        }

        await call.RequestStream.CompleteAsync();
        _ = await call.ResponseAsync;
        return NoContent();
    }

    // [HttpPut("Password")]
    // public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    // {
    //     await _identityApi.ChangePassword(User.GetUserId(), command);

    //     return NoContent();
    // }
}
