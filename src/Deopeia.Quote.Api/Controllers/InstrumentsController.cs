using Deopeia.Quote.Application.Instruments.GetInstrument;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class InstrumentsController : ApiController<InstrumentsController>
{
    [HttpGet("{symbol}")]
    public async Task<IActionResult> Get([FromRoute] string symbol)
    {
        var query = new GetInstrumentQuery(symbol);
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
