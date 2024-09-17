using Deopeia.Trading.Domain.Strategies;

namespace Deopeia.Trading.Infrastructure.Strategies;

internal class StrategyRepository(TradingContext context) : IStrategyRepository
{
    private readonly DbSet<Strategy> _strategys = context.Set<Strategy>();

    public async Task<Strategy> GetStrategyAsync(StrategyId strategyId)
    {
        var result = await _strategys.Include(x => x.Locales).SingleAsync(x => x.Id == strategyId);

        return result;
    }

    public void Add(Strategy strategy)
    {
        _strategys.Add(strategy);
    }
}
