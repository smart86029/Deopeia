namespace Deopeia.Trading.Domain.Orders.StopOrders;

public class StopOrder : Order
{
    public decimal StopPrice { get; private set; }

    public OrderId Triggeredby { get; private init; }
}
