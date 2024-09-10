using Deopeia.Identity.Application.Permissions.CreatePermission;
using Deopeia.Identity.Application.Permissions.GetPermission;
using Deopeia.Identity.Application.Permissions.GetPermissionOptions;
using Deopeia.Identity.Application.Permissions.GetPermissions;
using Deopeia.Identity.Application.Permissions.UpdatePermission;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPermissionViewModel>> Get([FromRoute] Guid id)
    {
        var query = new GetPermissionQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdatePermissionCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
