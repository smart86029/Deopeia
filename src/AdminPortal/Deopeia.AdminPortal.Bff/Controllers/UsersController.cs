namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class UsersController(User.UserClient client) : ApiController
{
    private readonly User.UserClient _client = client;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var request = new ListUserRequest();
        var response = await _client.ListUserAsync(request);
        return Ok(response);
    }
}
