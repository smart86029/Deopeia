namespace Deopeia.Quote.Domain.Instruments;

public interface IStockRepository : IRepository<Stock>
{
    Task AddAsync(IEnumerable<Stock> stocks);
}
