using Deopeia.AdminPortal.Bff.Models.Users;

namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class UsersController(User.UserClient client) : ApiController
{
    private readonly User.UserClient _client = client;

    [HttpGet]
    public async Task<ActionResult<PagedResult<UserDto>>> Get()
    {
        var request = new ListUserRequest();
        var grpcResponse = await _client.ListUserAsync(request);
        var response = grpcResponse.Adapt<PagedResult<UserDto>>();
        return Ok(response);
    }
}
