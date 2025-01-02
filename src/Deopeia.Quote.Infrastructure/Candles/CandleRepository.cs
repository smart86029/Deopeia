using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Infrastructure.Candles;

internal class CandleRepository(QuoteContext context) : ICandleRepository
{
    private readonly DbSet<Candle> _candles = context.Set<Candle>();
    private readonly DbSet<Tick> _ticks = context.Set<Tick>();

    public async Task<Candle?> GetCandleAsync(CandleId id)
    {
        return await _candles.FirstOrDefaultAsync(x =>
            x.Symbol == id.Symbol && x.TimeFrame == id.TimeFrame && x.Timestamp == id.Timestamp
        );
    }

    public async Task<ICollection<Tick>> GetTicksAsync(DateTimeOffset from, DateTimeOffset to)
    {
        return await _ticks.Where(x => x.Timestamp >= from && x.Timestamp < to).ToListAsync();
    }

    public async Task<bool> ExistsAsync(DateOnly date)
    {
        return await _candles.AnyAsync(x => x.Id.Timestamp == date.ToDateTimeOffset());
    }

    public async Task AddAsync(Candle candle)
    {
        await _candles.AddAsync(candle);
    }

    public async Task AddAsync(IEnumerable<Candle> candles)
    {
        await _candles.AddRangeAsync(candles);
    }

    public async Task AddAsync(Symbol symbol, Tick tick)
    {
        await _ticks.AddAsync(tick);
    }

    public async Task AddAsync(Symbol symbol, IEnumerable<Tick> ticks)
    {
        await _ticks.AddRangeAsync(ticks);
    }
}
