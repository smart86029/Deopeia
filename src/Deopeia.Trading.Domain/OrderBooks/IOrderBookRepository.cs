namespace Deopeia.Trading.Domain.OrderBooks;

public interface IOrderBookRepository : IRepository<OrderBook, Symbol>
{
    Task<OrderBook> GetOrderBookAsync(Symbol symbol);
}
