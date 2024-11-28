using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Domain.Orders.LimitOrders;

public class LimitOrder : Order
{
    private LimitOrder() { }

    internal LimitOrder(OrderSide side, decimal volume, Money price, AccountId createdBy)
        : base(OrderType.Limit, side, volume, price, createdBy)
    {
        LimitPrice = price;
    }

    public Money LimitPrice { get; private set; }
}
