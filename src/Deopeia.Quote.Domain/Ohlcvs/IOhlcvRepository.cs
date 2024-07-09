namespace Deopeia.Quote.Domain.Ohlcvs;

public interface IOhlcvRepository : IRepository<Ohlcv>
{
    Task<bool> ExistsAsync(DateOnly date);

    Task AddAsync(Ohlcv ohlcv);

    Task AddAsync(IEnumerable<Ohlcv> ohlcvs);
}
