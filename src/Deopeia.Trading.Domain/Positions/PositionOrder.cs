using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Positions;

public class PositionOrder(PositionId positionId, OrderId orderId)
    : Entity<PositionOrderId>(new PositionOrderId(positionId, orderId))
{
    public PositionId PositionId => Id.PositionId;

    public OrderId OrderId => Id.OrderId;
}
