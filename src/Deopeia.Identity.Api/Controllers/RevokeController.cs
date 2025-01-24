using Deopeia.Identity.Api.Models.Revoke;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
[Route("[controller]")]
[EnableCors(CorsPolicies.Oidc)]
public class RevokeController : ApiController<RevokeController>
{
    [HttpPost]
    public async Task<IActionResult> RevokeToken([FromForm] RevokeTokenRequest request)
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
            return BadRequest();
        }
    }
}
