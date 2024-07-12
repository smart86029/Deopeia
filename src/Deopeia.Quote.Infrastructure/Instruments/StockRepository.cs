using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class StockRepository(QuoteContext context) : IStockRepository
{
    private readonly QuoteContext _context = context;
    private readonly DbSet<Stock> _stocks = context.Set<Stock>();

    public async Task AddAsync(IEnumerable<Stock> stocks)
    {
        await _stocks.AddRangeAsync(stocks);
    }
}
