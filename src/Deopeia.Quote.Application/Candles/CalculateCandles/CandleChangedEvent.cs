namespace Deopeia.Quote.Application.Candles.CalculateCandles;

public record CandleChangedEvent(
    string Symbol,
    int TimeFrame,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) : Event { }
