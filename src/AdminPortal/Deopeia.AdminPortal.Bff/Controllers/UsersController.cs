using Deopeia.AdminPortal.Bff.Models.Users;

namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class UsersController(UserService.UserServiceClient client) : ApiController
{
    private readonly UserService.UserServiceClient _client = client;

    [HttpGet]
    public async Task<ActionResult<PagedResponse<User>>> Get([FromQuery] GetRequest request)
    {
        var grpcRequest = request.Adapt<ListUserRequest>();
        var grpcResponse = await _client.ListUserAsync(grpcRequest);
        var response = grpcResponse.Adapt<PagedResponse<User>>();
        return Ok(response);
    }
}
