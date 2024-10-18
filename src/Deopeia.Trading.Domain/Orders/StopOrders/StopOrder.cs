namespace Deopeia.Trading.Domain.Orders.StopOrders;

public class StopOrder : Order
{
    public Money StopPrice { get; private set; }

    public OrderId Triggeredby { get; private init; }
}
