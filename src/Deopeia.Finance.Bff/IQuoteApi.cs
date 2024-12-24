using Deopeia.Finance.Bff.Models.Candles;

namespace Deopeia.Finance.Bff;

public interface IQuoteApi
{
    [Get("/api/Candles/{symbol}/History")]
    Task<List<CandleDto>> GetCandlesAsync(string symbol);
}
