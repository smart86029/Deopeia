using Deopeia.Identity.Application.Roles.CreateRole;
using Deopeia.Identity.Application.Roles.GetRole;
using Deopeia.Identity.Application.Roles.GetRoles;
using Deopeia.Identity.Application.Roles.UpdateRole;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
public class RolesController : ApiController<RolesController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetRolesQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetRoleViewModel>> Get([FromRoute] Guid id)
    {
        var query = new GetRoleQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateRoleCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
