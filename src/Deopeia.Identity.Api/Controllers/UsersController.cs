using System.Net.Mime;
using Deopeia.Identity.Application.Users.ChangePassword;
using Deopeia.Identity.Application.Users.CreateUser;
using Deopeia.Identity.Application.Users.EnableAuthenticator;
using Deopeia.Identity.Application.Users.GetAuthenticator;
using Deopeia.Identity.Application.Users.GetAvatar;
using Deopeia.Identity.Application.Users.GetUser;
using Deopeia.Identity.Application.Users.GetUsers;
using Deopeia.Identity.Application.Users.UpdateUser;
using Deopeia.Identity.Application.Users.UploadAvatar;

namespace Deopeia.Identity.Api.Controllers;

[AllowAnonymous]
public class UsersController : ApiController<UsersController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetUsersQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserViewModel>> Get([FromRoute] Guid id)
    {
        var query = new GetUserQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateUserCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }

    [HttpGet("{id}/Authenticator")]
    public async Task<ActionResult<GetAuthenticatorResult>> GetAuthenticator([FromRoute] Guid id)
    {
        var query = new GetAuthenticatorQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPut("{id}/Authenticator")]
    public async Task<ActionResult> EnableAuthenticator(
        [FromRoute] Guid id,
        [FromBody] EnableAuthenticatorCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }

    [HttpGet("{id}/Avatar")]
    public async Task<IActionResult> GetAvatar([FromRoute] Guid id)
    {
        var query = new GetAvatarQuery(id);
        var result = await Sender.Send(query);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPut("{id}/Avatar")]
    public async Task<ActionResult> UploadAvatar(
        [FromRoute] Guid id,
        [FromBody] UploadAvatarCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }

    [HttpPut("{id}/Password")]
    public async Task<ActionResult> ChangePassword(
        [FromRoute] Guid id,
        [FromBody] ChangePasswordCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
