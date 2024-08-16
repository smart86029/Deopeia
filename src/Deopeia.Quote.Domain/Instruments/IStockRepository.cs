namespace Deopeia.Quote.Domain.Instruments;

public interface IStockRepository : IRepository<Stock, InstrumentId>
{
    Task AddAsync(IEnumerable<Stock> stocks);
}
