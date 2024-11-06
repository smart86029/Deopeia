using Deopeia.Quote.Application.Instruments.GetInstrument;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class InstrumentsController : ApiController<InstrumentsController>
{
    [HttpGet("{idOrSymbol}")]
    public async Task<IActionResult> Get([FromRoute] string idOrSymbol)
    {
        var query = new GetInstrumentQuery(idOrSymbol);
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
