using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Domain.Quotes;

public interface IQuoteRepository : IRepository<Ohlcv>
{
    void Add(Ohlcv quote);
}
