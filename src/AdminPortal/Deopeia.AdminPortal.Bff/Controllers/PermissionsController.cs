using Deopeia.AdminPortal.Bff.Models.Permissions;

namespace Deopeia.AdminPortal.Bff.Controllers;

[AllowAnonymous]
public class PermissionsController(PermissionService.PermissionServiceClient client) : ApiController
{
    private readonly PermissionService.PermissionServiceClient _client = client;

    [HttpGet("Options")]
    public async Task<ActionResult<IReadOnlyList<OptionResponse<string>>>> GetOptions()
    {
        var grpcRequest = new Empty();
        var grpcResponse = await _client.ListPermissionOptionAsync(grpcRequest);
        var options = grpcResponse.Items.Adapt<IReadOnlyList<OptionResponse<string>>>();
        return Ok(options);
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<Permission>>> Get([FromQuery] GetRequest request)
    {
        var grpcRequest = request.Adapt<ListPermissionRequest>();
        var grpcResponse = await _client.ListPermissionAsync(grpcRequest);
        var response = grpcResponse.Adapt<PagedResponse<Permission>>();
        return Ok(response);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<PermissionResponse>> Get([FromRoute] string code)
    {
        var grpcRequest = new GetPermissionRequest { Code = code };
        var grpcResponse = await _client.GetPermissionAsync(grpcRequest);
        var response = grpcResponse.Adapt<PermissionResponse>();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateRequest request)
    {
        var grpcRequest = request.Adapt<CreatePermissionRequest>();
        await _client.CreatePermissionAsync(grpcRequest);
        return NoContent();
    }

    [HttpPut("{code}")]
    public async Task<ActionResult> Update(
        [FromRoute] string code,
        [FromBody] UpdateRequest request
    )
    {
        var grpcRequest = request.Adapt<UpdatePermissionRequest>();
        grpcRequest.Code = code;
        await _client.UpdatePermissionAsync(grpcRequest);
        return NoContent();
    }

    [HttpDelete("{code}")]
    public async Task<ActionResult> Delete([FromRoute] string code)
    {
        var grpcRequest = new DeletePermissionRequest { Code = code };
        await _client.DeletePermissionAsync(grpcRequest);
        return NoContent();
    }
}
