using Deopeia.Quote.Application.Ohlcvs.GetHistoricalData;
using Microsoft.AspNetCore.Authorization;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class OhlcvsController : ApiController<OhlcvsController>
{
    [HttpGet("{symbol}/History")]
    public async Task<IActionResult> GetHistoricalData([FromRoute] string symbol)
    {
        var query = new GetHistoricalDataQuery(symbol);
        var result = await Sender.Send(query);

        return Ok(result);
    }
}
