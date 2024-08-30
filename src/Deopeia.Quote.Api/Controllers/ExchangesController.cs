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

    [HttpGet("{mic}")]
    public async Task<IActionResult> Get([FromRoute] string mic)
    {
        var query = new GetExchangeQuery(mic);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExchangeCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{mic}")]
    public async Task<IActionResult> Update(
        [FromRoute] string mic,
        [FromBody] UpdateExchangeCommand command
    )
    {
        command = command with { Mic = mic };
        await Sender.Send(command);

        return NoContent();
    }
}
