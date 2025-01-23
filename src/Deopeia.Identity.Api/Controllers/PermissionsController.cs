using Deopeia.Identity.Application.Permissions.CreatePermission;
using Deopeia.Identity.Application.Permissions.GetPermission;
using Deopeia.Identity.Application.Permissions.GetPermissionOptions;
using Deopeia.Identity.Application.Permissions.GetPermissions;
using Deopeia.Identity.Application.Permissions.UpdatePermission;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
public class PermissionsController : ApiController<PermissionsController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions()
    {
        var query = new GetPermissionOptionsQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPermissionsQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<GetPermissionViewModel>> Get([FromRoute] string code)
    {
        var query = new GetPermissionQuery(code);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(
        [FromRoute] string code,
        [FromBody] UpdatePermissionCommand command
    )
    {
        command = command with { Code = code };
        await Sender.Send(command);

        return NoContent();
    }
}
