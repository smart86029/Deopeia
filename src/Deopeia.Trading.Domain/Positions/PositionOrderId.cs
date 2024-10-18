using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Domain.Positions;

public readonly record struct PositionOrderId(PositionId PositionId, OrderId OrderId)
    : IEntityId { }
