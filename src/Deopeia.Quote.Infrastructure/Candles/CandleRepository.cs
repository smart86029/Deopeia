using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Infrastructure.Candles;

internal class CandleRepository(QuoteContext context) : ICandleRepository
{
    private readonly DbSet<Candle> _candles = context.Set<Candle>();

    public async Task<Candle?> GetCandleAsync(CandleId id)
    {
        return await _candles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(DateOnly date)
    {
        return await _candles.AnyAsync(x => x.Id.Timestamp == date.ToDateTimeOffset());
    }

    public async Task AddAsync(Candle ohlcv)
    {
        await _candles.AddAsync(ohlcv);
    }

    public async Task AddAsync(IEnumerable<Candle> ohlcvs)
    {
        await _candles.AddRangeAsync(ohlcvs);
    }
}
