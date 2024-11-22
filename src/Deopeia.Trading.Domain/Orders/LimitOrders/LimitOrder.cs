using Deopeia.Trading.Domain.Accounts;
using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Domain.Orders.LimitOrders;

public class LimitOrder : Order
{
    private LimitOrder() { }

    internal LimitOrder(
        PositionId positionId,
        OrderSide side,
        decimal volume,
        Money price,
        AccountId createdBy
    )
        : base(OrderType.Limit, positionId, side, volume, price, createdBy)
    {
        LimitPrice = price;
    }

    public Money LimitPrice { get; private set; }
}
