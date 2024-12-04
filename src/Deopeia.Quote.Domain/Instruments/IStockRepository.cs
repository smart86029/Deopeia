using Deopeia.Common.Domain.Finance;

namespace Deopeia.Quote.Domain.Instruments;

public interface IStockRepository : IRepository<Stock, Symbol>
{
    Task AddAsync(IEnumerable<Stock> stocks);

    Task<bool> Exists(string symbol);
}
