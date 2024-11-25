using Deopeia.Trading.Domain.Positions;

namespace Deopeia.Trading.Application.Positions.OpenPosition;

public record OpenPositionCommand(
    PositionType Type,
    Guid InstrumentId,
    decimal Volume,
    string CurrencyCode,
    decimal? Price,
    decimal? StopLossPrice,
    decimal? TakeProfitPrice,
    Guid AccountId
) : IRequest { }
