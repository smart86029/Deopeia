namespace Deopeia.Trading.Domain.MatchingEngines;

internal class OrderBookRepository : IOrderBookRepository
{
    private static readonly OrderBook orderBook = new();

    public Task<OrderBook> GetOrderBookAsync(Symbol symbol)
    {
        return Task.FromResult(orderBook);
    }
}
