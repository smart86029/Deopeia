namespace Deopeia.Quote.Domain.Exchanges;

public interface IExchangeRepository : IRepository<Exchange, ExchangeId>
{
    Task<Exchange> GetExchangeAsync(ExchangeId id);

    Task AddAsync(Exchange exchange);
}
