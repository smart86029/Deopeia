using Deopeia.AdminPortal.Bff.Models.Users;
using Deopeia.Identity.Contracts;

namespace Deopeia.AdminPortal.Bff.Controllers;

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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> Get([FromRoute] Guid id)
    {
        var grpcRequest = new GetUserRequest { Id = id };
        var grpcResponse = await _client.GetUserAsync(grpcRequest);
        var response = grpcResponse.Adapt<UserResponse>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRequest request)
    {
        var grpcRequest = request.Adapt<CreateUserRequest>();
        await _client.CreateUserAsync(grpcRequest);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRequest request)
    {
        var grpcRequest = request.Adapt<UpdateUserRequest>();
        grpcRequest.Id = id;
        await _client.UpdateUserAsync(grpcRequest);
        return NoContent();
    }
}
