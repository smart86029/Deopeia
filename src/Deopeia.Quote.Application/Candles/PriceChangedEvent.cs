namespace Deopeia.Quote.Application.Candles;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : Event { }
