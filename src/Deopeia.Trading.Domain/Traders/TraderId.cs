namespace Deopeia.Trading.Domain.Traders;

public readonly record struct TraderId(Guid Guid) : IEntityId
{
    public TraderId()
        : this(Guid.CreateVersion7()) { }
}
