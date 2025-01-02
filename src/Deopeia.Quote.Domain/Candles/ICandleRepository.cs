namespace Deopeia.Quote.Domain.Candles;

public interface ICandleRepository : IRepository<Candle, CandleId>
{
    Task<Candle?> GetCandleAsync(CandleId id);

    Task<ICollection<Tick>> GetTicksAsync(DateTimeOffset from, DateTimeOffset to);

    Task<bool> ExistsAsync(DateOnly date);

    Task AddAsync(Candle candle);

    Task AddAsync(IEnumerable<Candle> candles);

    Task AddAsync(Symbol Symbol, Tick tick);

    Task AddAsync(Symbol Symbol, IEnumerable<Tick> ticks);
}
