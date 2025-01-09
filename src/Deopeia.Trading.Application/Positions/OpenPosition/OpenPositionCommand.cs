using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Application.Positions.OpenPosition;

public record OpenPositionCommand(
    PositionType Type,
    string Symbol,
    decimal Volume,
    string CurrencyCode,
    decimal? Price,
    decimal? StopLossPrice,
    decimal? TakeProfitPrice,
    Guid TraderId
) : IRequest { }
