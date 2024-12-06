using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Domain.Orders;

public abstract class Order : AggregateRoot<OrderId>
{
    protected Order() { }

    protected Order(
        OrderType type,
        OrderSide side,
        decimal volume,
        Money price,
        AccountId createdBy
    )
    {
        Type = type;
        Side = side;
        Volume = volume;
        UnfilledVolume = volume;
        Price = price;
        CreatedBy = createdBy;
    }

    public OrderType Type { get; private init; }

    public PositionId PositionId { get; private init; }

    public OrderSide Side { get; private init; }

    public decimal Volume { get; private set; }

    public decimal UnfilledVolume { get; private set; }

    public Money Price { get; private set; }

    public AccountId CreatedBy { get; private init; }

    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ExecutedAt { get; private init; }

    public void Fill(decimal volume)
    {
        UnfilledVolume -= volume;
    }
}
