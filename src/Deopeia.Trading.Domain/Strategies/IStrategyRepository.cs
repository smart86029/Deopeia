namespace Deopeia.Trading.Domain.Strategies;

public interface IStrategyRepository : IRepository<Strategy, StrategyId>
{
    Task<Strategy> GetStrategyAsync(StrategyId strategyId);

    void Add(Strategy strategy);
}
