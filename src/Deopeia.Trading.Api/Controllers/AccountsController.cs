using Deopeia.Trading.Application.Accounts.CreateAccount;
using Deopeia.Trading.Application.Accounts.Deposit;
using Deopeia.Trading.Application.Accounts.GetAccount;
using Deopeia.Trading.Application.Accounts.GetAccounts;
using Deopeia.Trading.Application.Accounts.UpdateAccount;
using Deopeia.Trading.Application.Accounts.Withdraw;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class AccountsController : ApiController<AccountsController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAccountsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var query = new GetAccountQuery(id);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
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
        [FromBody] UpdateAccountCommand command
    )
    {
        command = command with { Id = id };
        await Sender.Send(command);

        return NoContent();
    }
}
