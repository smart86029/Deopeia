using Deopeia.AdminPortal.Bff.Models.Roles;

namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class RolesController(RoleService.RoleServiceClient client) : ApiController
{
    private readonly RoleService.RoleServiceClient _client = client;

    [HttpGet("Options")]
    public async Task<ActionResult<IReadOnlyList<OptionResponse<string>>>> GetOptions()
    {
        var grpcRequest = new Empty();
        var grpcResponse = await _client.ListRoleOptionAsync(grpcRequest);
        var options = grpcResponse.Items.Adapt<IReadOnlyList<OptionResponse<string>>>();
        return Ok(options);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Role>>> Get([FromQuery] GetRequest request)
    {
        var grpcRequest = request.Adapt<ListRoleRequest>();
        var grpcResponse = await _client.ListRoleAsync(grpcRequest);
        var response = grpcResponse.Adapt<PagedResponse<Role>>();
        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<RoleResponse>> Get([FromRoute] string code)
    {
        var grpcRequest = new GetRoleRequest { Code = code };
        var grpcResponse = await _client.GetRoleAsync(grpcRequest);
        var response = grpcResponse.Adapt<RoleResponse>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRequest request)
    {
        var grpcRequest = request.Adapt<CreateRoleRequest>();
        await _client.CreateRoleAsync(grpcRequest);
        return NoContent();
    }

    [HttpPut("{code}")]
    public async Task<ActionResult> Update(
        [FromRoute] string code,
        [FromBody] UpdateRequest request
    )
    {
        var grpcRequest = request.Adapt<UpdateRoleRequest>();
        grpcRequest.Code = code;
        await _client.UpdateRoleAsync(grpcRequest);
        return NoContent();
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult> Delete([FromRoute] string code)
    {
        var grpcRequest = new DeleteRoleRequest { Code = code };
        await _client.DeleteRoleAsync(grpcRequest);
        return NoContent();
    }
}
