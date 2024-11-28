namespace Deopeia.Trading.Domain.MatchingEngines;

internal class OrderBookRepository : IOrderBookRepository
{
    private static readonly OrderBook orderBook = new();

    public Task<OrderBook> GetOrderBookAsync(InstrumentId instrumentId)
    {
        return Task.FromResult(orderBook);
    }
}
