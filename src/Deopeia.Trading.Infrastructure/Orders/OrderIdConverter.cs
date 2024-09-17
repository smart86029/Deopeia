using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Infrastructure.Orders;

internal class OrderIdConverter()
    : ValueConverter<OrderId, Guid>(id => id.Guid, guid => new OrderId(guid)) { }
