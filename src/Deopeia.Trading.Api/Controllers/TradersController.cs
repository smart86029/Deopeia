using Deopeia.Trading.Application.Traders.Deposit;
using Deopeia.Trading.Application.Traders.Dislike;
using Deopeia.Trading.Application.Traders.GetAccounts;
using Deopeia.Trading.Application.Traders.GetFavorites;
using Deopeia.Trading.Application.Traders.GetTrader;
using Deopeia.Trading.Application.Traders.GetTraders;
using Deopeia.Trading.Application.Traders.Like;
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

    [HttpGet("{id}/Accounts")]
    public async Task<IActionResult> GetAccounts([FromRoute] Guid id)
    {
        var query = new GetAccountsQuery(id);
        var results = await Sender.Send(query);

        return Ok(results);
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

    [HttpGet("{id}/Favorites")]
    public async Task<IActionResult> GetFavorites([FromRoute] Guid id)
    {
        var query = new GetFavoritesQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPut("{id}/Favorites/{symbol}")]
    public async Task<IActionResult> Like([FromRoute] Guid id, [FromRoute] string symbol)
    {
        var command = new LikeCommand(id, symbol);
        await Sender.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}/Favorites/{symbol}")]
    public async Task<IActionResult> Dislike([FromRoute] Guid id, [FromRoute] string symbol)
    {
        var command = new DislikeCommand(id, symbol);
        await Sender.Send(command);

        return NoContent();
    }
}
