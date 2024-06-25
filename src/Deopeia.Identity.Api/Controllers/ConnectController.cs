using Deopeia.Identity.Api.Models.Connect;
using Deopeia.Identity.Application.Connect;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
[Route("[controller]")]
[EnableCors(CorsPolicies.Oidc)]
public class ConnectController : ApiController<ConnectController>
{
    [HttpGet("Authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizeRequest request)
    {
        try
        {
            var command = request.ToCommand();
            var authorizeResult = await Sender.Send(command);

            if (User.Identity?.IsAuthenticated ?? false)
            {
                return Redirect(authorizeResult.ReturnUrl);
            }

            return RedirectToPage(
                "/Authentication/SignIn",
                new { authorizeResult.Code, authorizeResult.ReturnUrl }
            );
        }
        catch (Exception exception)
        {
            var url =
                $"{request.RedirectUri}?error={exception.Message}&error_description=&state={request.State}";

            return Redirect(url);
        }
    }

    [HttpPost("Token")]
    public async Task<IActionResult> GenerateToken([FromForm] GenerateTokenRequest request)
    {
        try
        {
            var command = request.ToCommand();
            var result = await Sender.Send(command);
            if (result.Error is not null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest(new ErrorResult());
        }
    }
}
