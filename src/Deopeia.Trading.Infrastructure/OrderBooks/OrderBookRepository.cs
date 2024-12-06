using Deopeia.Trading.Domain.OrderBooks;

namespace Deopeia.Trading.Infrastructure.OrderBooks;

internal class OrderBookRepository : IOrderBookRepository
{
    private static readonly Dictionary<Symbol, OrderBook> orderBooks = [];

    public Task<OrderBook> GetOrderBookAsync(Symbol symbol)
    {
        if (!orderBooks.TryGetValue(symbol, out var orderBook))
        {
            orderBook = new OrderBook(symbol);
            orderBooks.Add(symbol, orderBook);
        }

        return Task.FromResult(orderBook);
    }
}
