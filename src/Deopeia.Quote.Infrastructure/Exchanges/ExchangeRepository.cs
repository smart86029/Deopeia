using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges;

internal class ExchangeRepository(QuoteContext context) : IExchangeRepository
{
    private readonly DbSet<Exchange> _exchanges = context.Set<Exchange>();

    public Task<Exchange> GetExchangeAsync(ExchangeId id)
    {
        return _exchanges.Include(x => x.Locales).SingleAsync(x => x.Id == id);
    }

    public Task<Exchange> GetExchangeAsync(string code)
    {
        return _exchanges.Include(x => x.Locales).SingleAsync(x => x.Code == code);
    }

    public async Task AddAsync(Exchange exchange)
    {
        await _exchanges.AddAsync(exchange);
    }
}
