using Deopeia.Identity.Api.Models.Revoke;

namespace Deopeia.Identity.Api.Controllers;

[Route("[controller]")]
[EnableCors(CorsPolicies.Oidc)]
public class RevokeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> RevokeToken([FromForm] RevokeTokenRequest request)
    {
        try
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);
            return result.Error is null ? Ok(result) : BadRequest(result.Error);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}
