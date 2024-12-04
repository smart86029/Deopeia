using Deopeia.Trading.Application.Contracts.CreateContract;
using Deopeia.Trading.Application.Contracts.GetContract;
using Deopeia.Trading.Application.Contracts.GetContractOptions;
using Deopeia.Trading.Application.Contracts.GetContracts;
using Deopeia.Trading.Application.Contracts.UpdateContract;

namespace Deopeia.Trading.Api.Controllers;

[AllowAnonymous]
public class ContractsController : ApiController<ContractsController>
{
    [HttpGet("Options")]
    public async Task<IActionResult> GetOptions([FromQuery] GetContractOptionsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetContractsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> Get([FromRoute] string symbol)
    {
        var query = new GetContractQuery(symbol);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateContractCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{symbol}")]
    public async Task<IActionResult> Update(
        [FromRoute] string symbol,
        [FromBody] UpdateContractCommand command
    )
    {
        command = command with { Symbol = symbol };
        await Sender.Send(command);

        return NoContent();
    }
}
