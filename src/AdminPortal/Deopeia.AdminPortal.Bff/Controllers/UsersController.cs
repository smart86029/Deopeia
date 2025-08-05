namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class UsersController : ApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}
