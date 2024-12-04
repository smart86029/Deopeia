namespace Deopeia.Trading.Domain.MatchingEngines;

public interface IOrderBookRepository : IRepository<OrderBook, Symbol>
{
    Task<OrderBook> GetOrderBookAsync(Symbol symbol);
}
