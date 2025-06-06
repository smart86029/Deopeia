using Deopeia.ClientPortal.Bff.Models.Quotes;
using Deopeia.Finance.Bff.Models.Positions;
using Deopeia.Finance.Bff.Models.Trading;

namespace Deopeia.ClientPortal.Bff.Controllers;

public class PositionsController(IQuoteApi quoteApi, ITradingApi tradingApi) : ApiController
{
    private readonly IQuoteApi _quoteApi = quoteApi;
    private readonly ITradingApi _tradingApi = tradingApi;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetPositionsQuery query)
    {
        var result = await _tradingApi.GetPositionsAsync(query);
        foreach (var position in result.Items)
        {
            var candles = await _quoteApi.GetCandlesAsync(position.Symbol);
            var contract = await _tradingApi.GetContractAsync(position.Symbol);
            position.Name = contract
                .Locales.Single(x => x.Culture == CultureInfo.CurrentCulture.Name)
                .Name;

            var last = candles.LastOrDefault();
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
