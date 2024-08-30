using Deopeia.Quote.Domain.Exchanges;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentRepository(QuoteContext context) : IInstrumentRepository
{
    private readonly DbSet<Instrument> _instruments = context.Set<Instrument>();

    public async Task<IReadOnlyDictionary<string, InstrumentId>> GetSymbolMap(ExchangeId exchangeId)
    {
        return await _instruments
            .Where(x => x.ExchangeId == exchangeId)
            .ToDictionaryAsync(x => x.Symbol, x => x.Id);
    }
}
