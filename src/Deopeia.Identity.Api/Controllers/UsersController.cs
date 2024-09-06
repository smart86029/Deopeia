using Deopeia.Identity.Application.Users.GetUsers;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
public class UsersController : ApiController<UsersController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetUsersQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }
}
