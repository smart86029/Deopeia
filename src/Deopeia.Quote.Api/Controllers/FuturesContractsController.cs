using Deopeia.Quote.Application.FuturesContracts.CreateFuturesContract;
using Deopeia.Quote.Application.FuturesContracts.GetFuturesContract;
using Deopeia.Quote.Application.FuturesContracts.GetFuturesContracts;
using Deopeia.Quote.Application.FuturesContracts.UpdateFuturesContract;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class FuturesContractsController : ApiController<FuturesContractsController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetFuturesContractsQuery query)
    {
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{mic}")]
    public async Task<IActionResult> Get([FromRoute] string mic)
    {
        var query = new GetFuturesContractQuery(mic);
        var result = await Sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFuturesContractCommand command)
    {
        await Sender.Send(command);

        return Created();
    }

    [HttpPut("{mic}")]
    public async Task<IActionResult> Update(
        [FromRoute] string mic,
        [FromBody] UpdateFuturesContractCommand command
    )
    {
        command = command with { Mic = mic };
        await Sender.Send(command);

        return NoContent();
    }
}
