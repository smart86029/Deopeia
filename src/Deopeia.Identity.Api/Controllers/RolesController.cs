using Deopeia.Identity.Application.Roles.CreateRole;
using Deopeia.Identity.Application.Roles.GetRole;
using Deopeia.Identity.Application.Roles.GetRoleOptions;
using Deopeia.Identity.Application.Roles.GetRoles;
using Deopeia.Identity.Application.Roles.UpdateRole;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
public class RolesController : ApiController<RolesController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions()
    {
        var query = new GetRoleOptionsQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetRolesQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<GetRoleViewModel>> Get([FromRoute] string code)
    {
        var query = new GetRoleQuery(code);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{code}")]
    public async Task<IActionResult> Update(
        [FromRoute] string code,
        [FromBody] UpdateRoleCommand command
    )
    {
        command = command with { Code = code };
        await Sender.Send(command);

        return NoContent();
    }
}
