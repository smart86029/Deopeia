using Deopeia.Trading.Application.Positions.GetPositions;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class PositionsController : ApiController<PositionsController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPositionsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> Get([FromRoute] Guid id)
    //{
    //    var query = new GetPositionQuery(id);
    //    var result = await Sender.Send(query);

    //    return Ok(result);
    //}


    //[HttpPut("{id}")]
    //public async Task<IActionResult> Update(
    //    [FromRoute] Guid id,
    //    [FromBody] UpdatePositionCommand command
    //)
    //{
    //    command = command with { Id = id };
    //    await Sender.Send(command);

    //    return NoContent();
    //}
}
