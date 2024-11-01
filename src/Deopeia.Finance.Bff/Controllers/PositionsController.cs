namespace Deopeia.Finance.Bff.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController(IQuoteApi quoteApi, ITradingApi tradingApi) : ControllerBase
{
    private readonly IQuoteApi _quoteApi = quoteApi;
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _tradingApi.GetPositionsAsync();
        foreach (var position in result.Items)
        {
            var candles = await _quoteApi.GetCandlesAsync(position.InstrumentId);
            var last = candles.Quotes.LastOrDefault();
            if (last is not null)
            {
                position.Price = last.Open;

                var sign = position.Type == 0 ? 1 : -1;
                position.UnrealisedPnL = (position.Price - position.OpenPrice) * 1000 * sign;
            }
        }

        return Ok(result);
    }
}
