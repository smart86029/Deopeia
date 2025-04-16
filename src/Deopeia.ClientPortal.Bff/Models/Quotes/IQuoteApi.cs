using Deopeia.ClientPortal.Bff.Models.Candles;

namespace Deopeia.ClientPortal.Bff.Models.Quotes
{
    public interface IQuoteApi
    {
        [Get("/api/Candles/{symbol}/History")]
        Task<List<CandleDto>> GetCandlesAsync(string symbol);
    }
}
