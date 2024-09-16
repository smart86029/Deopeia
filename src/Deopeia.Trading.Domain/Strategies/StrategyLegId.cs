namespace Deopeia.Trading.Domain.Strategies;

public readonly record struct StrategyLegId(StrategyId StrategyId, int SerialNumber) : IEntityId { }
