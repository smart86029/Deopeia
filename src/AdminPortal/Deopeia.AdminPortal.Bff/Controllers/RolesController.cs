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
}
