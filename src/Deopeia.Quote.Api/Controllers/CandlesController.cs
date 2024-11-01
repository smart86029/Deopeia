using Deopeia.Quote.Application.Candles.GetHistoricalData;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class CandlesController : ApiController<CandlesController>
{
    [HttpGet("{idOrSymbol}/History")]
    public async Task<ActionResult<GetHistoricalDataViewModel>> GetHistoricalData(
        [FromRoute] string idOrSymbol
    )
    {
        var query = new GetHistoricalDataQuery(idOrSymbol);
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
