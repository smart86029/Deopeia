namespace Deopeia.Trading.Domain.Orders.StopOrders;

public class StopOrder : Order
{
    private StopOrder() { }

    public StopOrder(Money stopPrice, OrderId triggeredby)
    {
        StopPrice = stopPrice;
        Triggeredby = triggeredby;
    }

    public Money StopPrice { get; private set; }

    public OrderId Triggeredby { get; private init; }
}
