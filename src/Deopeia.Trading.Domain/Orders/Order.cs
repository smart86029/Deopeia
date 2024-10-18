using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Domain.Orders;

public abstract class Order : AggregateRoot<OrderId>
{
    public OrderType Type { get; private init; }

    public InstrumentId InstrumentId { get; init; }

    public OrderSide Side { get; private init; }

    public OrderStatus Status { get; private set; }

    public decimal Volume { get; private set; }

    public Money Price { get; private set; }

    public AccountId CreatedBy { get; private init; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ExecutedAt { get; private init; }
}
