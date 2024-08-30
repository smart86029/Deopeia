using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Domain.Instruments;

public interface IInstrumentRepository : IRepository<Instrument, InstrumentId>
{
    Task<IReadOnlyDictionary<string, InstrumentId>> GetSymbolMap(ExchangeId exchangeId);
}
