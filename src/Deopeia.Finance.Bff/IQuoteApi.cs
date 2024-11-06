using Deopeia.Finance.Bff.Models.Positions;
using Deopeia.Finance.Bff.Models.Quotes;

namespace Deopeia.Finance.Bff;

public interface IQuoteApi
{
    [Get("/api/Candles/{id}/History")]
    Task<GetHistoricalDataViewModel> GetCandlesAsync(Guid id);

    [Get("/api/Instruments/{idOrSymbol}")]
    Task<GetInstrumentViewModel> GetInstrumentAsync(string idOrSymbol);
}
