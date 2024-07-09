using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Infrastructure.Ohlcvs;

internal class OhlcvRepository(QuoteContext context) : IOhlcvRepository
{
    private readonly QuoteContext _context = context;
    private readonly DbSet<Ohlcv> _ohlcvs = context.Set<Ohlcv>();

    public async Task<bool> ExistsAsync(DateOnly date)
    {
        return await _ohlcvs.AnyAsync(x => x.RecordedAt == date.ToDateTimeOffset());
    }

    public async Task AddAsync(Ohlcv ohlcv)
    {
        await _ohlcvs.AddAsync(ohlcv);
    }

    public async Task AddAsync(IEnumerable<Ohlcv> ohlcvs)
    {
        await _ohlcvs.AddRangeAsync(ohlcvs);
    }
}
