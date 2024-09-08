namespace Deopeia.Trading.Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    public OrderType Type { get; private init; }

    // Symbol
    //public InstrumentId InstrumentId { get; init; }

    public OrderSide Side { get; private init; }

    public OrderStatus Status { get; private set; }

    public decimal? Price { get; private set; }

    //public OrderId?

    public Guid CreatedBy { get; private init; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ExecutedAt { get; private init; }
}
