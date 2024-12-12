using Deopeia.Finance.Bff.Models.Positions;
using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff;

public interface IQuoteApi
{
    [Get("/api/Candles/{symbol}/History")]
    Task<GetHistoricalDataViewModel> GetCandlesAsync(string symbol);

    [Get("/api/Instruments/{idOrSymbol}")]
    Task<GetInstrumentViewModel> GetInstrumentAsync(string idOrSymbol);
}
