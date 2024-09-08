namespace Deopeia.Trading.Domain.Orders;

public readonly record struct OrderId(Guid Guid) : IEntityId
{
    public OrderId()
        : this(GuidUtility.NewGuid()) { }
}
