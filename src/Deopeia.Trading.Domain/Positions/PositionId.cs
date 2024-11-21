namespace Deopeia.Trading.Domain.Positions;

public readonly record struct PositionId(Guid Guid) : IEntityId
{
    public PositionId()
        : this(Guid.CreateVersion7()) { }
}
