using Deopeia.Trading.Application.Traders.CreateTrader;
using Deopeia.Trading.Application.Traders.Deposit;
using Deopeia.Trading.Application.Traders.GetTrader;
using Deopeia.Trading.Application.Traders.GetTraders;
using Deopeia.Trading.Application.Traders.UpdateTrader;
using Deopeia.Trading.Application.Traders.Withdraw;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class TradersController : ApiController<TradersController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetTradersQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetTraderQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTraderCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPost("{id}/Deposit")]
    public async Task<IActionResult> Deposit([FromRoute] Guid id, [FromBody] DepositCommand command)
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return Created();
    }

    [HttpPost("{id}/Withdraw")]
    public async Task<IActionResult> Withdraw(
        [FromRoute] Guid id,
        [FromBody] WithdrawCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateTraderCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
