using Deopeia.Trading.Application.Strategies.CreateStrategy;
using Deopeia.Trading.Application.Strategies.GetStrategies;
using Deopeia.Trading.Application.Strategies.GetStrategy;
using Deopeia.Trading.Application.Strategies.UpdateStrategy;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class StrategiesController : ApiController<StrategiesController>
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetStrategiesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetStrategyQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStrategyCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateStrategyCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
