using Deopeia.Trading.Domain.Orders;

namespace Deopeia.Trading.Application.Orders.PlaceOrder;

public record PlaceOrderCommand(
    OrderSide Side,
    string Symbol,
    decimal Volume,
    string CurrencyCode,
    decimal? Price,
    decimal? StopLossPrice,
    decimal? TakeProfitPrice,
    Guid AccountId
) : IRequest { }
