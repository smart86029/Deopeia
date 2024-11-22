using Deopeia.Trading.Application.Positions.GetPosition;
using Deopeia.Trading.Application.Positions.GetPositions;
using Deopeia.Trading.Application.Positions.OpenPosition;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetPositionQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Open([FromBody] OpenPositionCommand command)
    {
        await Sender.Send(command);

        return NoContent();
    }
}
