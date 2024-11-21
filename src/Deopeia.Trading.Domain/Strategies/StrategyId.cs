namespace Deopeia.Trading.Domain.Strategies;

public readonly record struct StrategyId(Guid Guid) : IEntityId
{
    public StrategyId()
        : this(Guid.CreateVersion7()) { }
}
