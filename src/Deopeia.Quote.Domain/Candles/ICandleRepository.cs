namespace Deopeia.Quote.Domain.Candles;

public interface ICandleRepository : IRepository<Candle, CandleId>
{
    Task<Candle?> GetCandleAsync(CandleId id);

    Task<bool> ExistsAsync(DateOnly date);

    Task AddAsync(Candle candle);

    Task AddAsync(IEnumerable<Candle> candles);
}
