namespace Deopeia.Trading.Domain.Orders.LimitOrders;

public class LimitOrder : Order
{
    public Money LimitPrice { get; private set; }
}
