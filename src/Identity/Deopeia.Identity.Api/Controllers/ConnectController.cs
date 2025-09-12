using Deopeia.Identity.Api.Models.Connect;
using Deopeia.Identity.Application.Connect;

namespace Deopeia.Identity.Api.Controllers;

public class ConnectController(IMediator mediator) : OidcController
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("Authorize")]
    public async Task<IActionResult> Authorize([FromQuery] AuthorizeRequest request)
    {
        try
        {
            var userId = User
                .Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value.ToGuid();
            var command = request.ToCommand(userId);
            var authorizeResult = await _mediator.Send(command);

            if (User.Identity is null || !User.Identity.IsAuthenticated)
            {
                return RedirectToPage(
                    "/Authentication/SignIn",
                    new { authorizeResult.Code, authorizeResult.ReturnUrl }
                );
            }

            return Redirect(authorizeResult.ReturnUrl);
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
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result.Error);
        }
        catch (Exception exception)
        {
            return BadRequest(
                new ErrorResult
                {
                    Error = exception.Message,
                    ErrorDescription = exception.StackTrace ?? string.Empty,
                }
            );
        }
    }

    [HttpGet("EndSession")]
    public async Task<IActionResult> EndSession([FromQuery] EndSessionRequest request)
    {
        if (User.Identity!.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }

        var command = request.ToCommand();
        var result = await _mediator.Send(command);
        return Redirect(result.ReturnUrl);
    }

    [HttpPost("EndSession")]
    public async Task<IActionResult> PostEndSession([FromBody] EndSessionRequest request)
    {
        if (User.Identity!.IsAuthenticated)
        {
            await HttpContext.SignOutAsync();
        }

        var command = request.ToCommand();
        var result = await _mediator.Send(command);
        return Redirect(result.ReturnUrl);
    }
}
