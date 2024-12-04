namespace Deopeia.Quote.Domain.Instruments;

public interface IInstrumentRepository : IRepository<Instrument, Symbol>
{
    Task<IReadOnlyDictionary<string, Symbol>> GetSymbolMapAsync(ExchangeId exchangeId);
}
