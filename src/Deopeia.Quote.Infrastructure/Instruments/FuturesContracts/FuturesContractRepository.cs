using Deopeia.Quote.Domain.Instruments.FuturesContracts;

namespace Deopeia.Quote.Infrastructure.Instruments.FuturesContracts;

internal class FuturesContractRepository(QuoteContext context) : IFuturesContractRepository
{
    private readonly DbSet<FuturesContract> _futuresContracts = context.Set<FuturesContract>();

    public async Task<FuturesContract> GetFuturesContractAsync(InstrumentId id)
    {
        return await _futuresContracts.Include(x => x.Locales).FirstAsync(x => x.Id == id);
    }

    public async Task AddAsync(FuturesContract futuresContract)
    {
        await _futuresContracts.AddAsync(futuresContract);
    }

    public async Task<bool> Exists(string symbol)
    {
        return await _futuresContracts.AnyAsync(x => x.Symbol == symbol);
    }
}
