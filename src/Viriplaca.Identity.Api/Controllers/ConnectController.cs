using Viriplaca.Identity.Api.Models.Connect;
using Viriplaca.Identity.App.Connect.Authorize;
using Viriplaca.Identity.App.Connect.GenerateToken;

namespace Viriplaca.Identity.Api.Controllers;

[Route("[controller]")]
public class ConnectController : ApiController<ConnectController>
{
    [HttpGet("Authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizeRequest request)
    {
        try
        {
            var command = new AuthorizeCommand(
                request.ResponseType,
                request.ClientId,
                request.RedirectUri,
                request.Scopes,
                request.State);
            var authorizeResult = await Sender.Send(command);

            if (User.Identity?.IsAuthenticated ?? false)
            {
                var token = await Sender.Send(new GenerateTokenCommand(authorizeResult.Code, User));
                var url = $"{request.RedirectUri}#{token}";

                return Redirect(url);
            }

            var model = new
            {
                ReturnUrl = request.RedirectUri,
                authorizeResult.Code,
            };

            return RedirectToPage("/Authentication/SignIn", model);
        }
        catch (Exception exception)
        {
            var url = $"{request.RedirectUri}?error={exception.Message}&error_description=&state={request.State}";

            return Redirect(url);
        }
    }

    [HttpPost("Token")]
    public async Task<IActionResult> Token([FromQuery] TokenRequest request)
    {
        var command = new GenerateTokenCommand(request.Code, User);
        var result = await Sender.Send(command);

        return Ok(result);
    }
}
