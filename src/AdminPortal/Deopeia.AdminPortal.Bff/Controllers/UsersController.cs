namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class UsersController(User.UserClient client) : ApiController
{
    private readonly User.UserClient _client = client;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _client.GetUserAsync(new GetUserRequest { Id = "12345" });
        return Ok(response.Name);
    }
}
