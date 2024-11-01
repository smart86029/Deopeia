namespace Deopeia.Quote.Domain.Instruments;

public interface IInstrumentRepository : IRepository<Instrument, InstrumentId>
{
    Task<IReadOnlyDictionary<string, InstrumentId>> GetSymbolMapAsync(ExchangeId exchangeId);
}
