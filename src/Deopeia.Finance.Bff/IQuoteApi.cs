using Deopeia.Finance.Bff.Models.Positions;

namespace Deopeia.Finance.Bff;

public interface IQuoteApi
{
    [Get("/api/Candles/{id}/History")]
    Task<GetHistoricalDataViewModel> GetCandlesAsync(Guid id);
}
