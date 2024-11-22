using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Orders;
using Deopeia.Trading.Domain.Orders.LimitOrders;
using Deopeia.Trading.Domain.Orders.MarketOrders;
using Deopeia.Trading.Domain.Orders.StopOrders;

namespace Deopeia.Trading.Domain.Positions;

public class Position : AggregateRoot<PositionId>
{
    private readonly List<Order> _orders = [];

    private Position() { }

    public Position(
        PositionType type,
        InstrumentId instrumentId,
        decimal volume,
        Money? openPrice,
        Money? stopLimitPrice,
        Money? takeProfitPrice
    )
    {
        type.MustBeDefined();
        volume.MustGreaterThan(0);

        Type = type;
        InstrumentId = instrumentId;
        Volume = volume;
        OpenPrice = openPrice ?? new Money();

        var side = type == PositionType.Long ? OrderSide.Buy : OrderSide.Sell;

        if (openPrice.HasValue)
        {
            _orders.Add(new LimitOrder(Id, side, volume, OpenPrice, OpenedBy));
        }
        else
        {
            _orders.Add(new MarketOrder());
        }

        var orderId = _orders[0].Id;
        if (stopLimitPrice.HasValue)
        {
            _orders.Add(new StopOrder(stopLimitPrice.Value, orderId));
        }

        if (takeProfitPrice.HasValue)
        {
            _orders.Add(new StopOrder(takeProfitPrice.Value, orderId));
        }
    }

    public PositionType Type { get; private init; }

    public InstrumentId InstrumentId { get; init; }

    public OrderStatus Status { get; private set; }

    public decimal Volume { get; private set; }

    public Money OpenPrice { get; private set; }

    public Money Margin { get; private set; } = new();

    public AccountId OpenedBy { get; private init; }

    public DateTimeOffset OpenedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ClosedAt { get; private set; }

    public bool IsClosed => ClosedAt.HasValue;

    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
}
