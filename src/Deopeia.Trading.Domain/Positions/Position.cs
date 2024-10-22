using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Positions;

public class Position : AggregateRoot<PositionId>
{
    private readonly List<PositionOrder> _positionOrders = [];

    public PositionType Type { get; private init; }

    public InstrumentId InstrumentId { get; init; }

    public OrderStatus Status { get; private set; }

    public decimal Volume { get; private set; }

    public Money Margin { get; private set; }

    public Money OpenPrice { get; private set; }

    public AccountId OpenedBy { get; private init; }

    public DateTimeOffset OpenedAt { get; private init; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? ClosedAt { get; private set; }

    public bool IsClosed => ClosedAt.HasValue;

    public IReadOnlyCollection<PositionOrder> PositionOrders => _positionOrders.AsReadOnly();
}
