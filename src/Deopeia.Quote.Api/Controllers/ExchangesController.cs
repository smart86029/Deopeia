using Deopeia.Quote.Application.Exchanges.CreateExchange;
using Deopeia.Quote.Application.Exchanges.GetExchange;
using Deopeia.Quote.Application.Exchanges.GetExchanges;
using Deopeia.Quote.Application.Exchanges.UpdateExchange;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class ExchangesController : ApiController<ExchangesController>
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new GetExchangesQuery();
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetExchangeQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExchangeCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateExchangeCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
