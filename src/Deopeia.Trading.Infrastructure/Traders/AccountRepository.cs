using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class AccountRepository(TradingContext context) : ITraderRepository
{
    private readonly DbSet<Trader> _traders = context.Set<Trader>();

    public async Task<IReadOnlyList<Trader>> GetTradersAsync()
    {
        return await _traders.ToListAsync();
    }

    public async Task<Trader> GetTraderAsync(TraderId id)
    {
        return await _traders.SingleAsync(x => x.Id == id);
    }

    public async Task AddAsync(Trader trader)
    {
        await _traders.AddAsync(trader);
    }
}
