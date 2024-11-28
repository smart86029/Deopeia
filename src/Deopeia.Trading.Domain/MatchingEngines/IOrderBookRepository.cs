namespace Deopeia.Trading.Domain.MatchingEngines;

public interface IOrderBookRepository : IRepository<OrderBook, InstrumentId>
{
    Task<OrderBook> GetOrderBookAsync(InstrumentId instrumentId);
}
