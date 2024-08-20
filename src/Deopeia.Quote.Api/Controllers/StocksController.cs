using Deopeia.Quote.Application.Stocks.GetStocks;

namespace Deopeia.Quote.Api.Controllers;

[AllowAnonymous]
public class StocksController : ApiController<StocksController>
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetStocksQuery query)
    {
        var results = await Sender.Send(query);

        return Ok(results);
    }
}
