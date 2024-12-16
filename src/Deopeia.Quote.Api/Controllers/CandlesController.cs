using Deopeia.Quote.Application.Candles.GetHistoricalData;
using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class CandlesController : ApiController<CandlesController>
{
    [HttpGet("{symbol}/History")]
    public async Task<ActionResult<List<CandleDto>>> GetHistoricalData(
        [FromRoute] string symbol,
        [FromQuery] TimeFrame timeFrame,
        [FromQuery] DateTimeOffset? startedAt
    )
    {
        var query = new GetHistoricalDataQuery(symbol, timeFrame, startedAt);
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
