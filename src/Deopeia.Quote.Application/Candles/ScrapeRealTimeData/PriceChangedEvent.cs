namespace Deopeia.Quote.Application.Candles.ScrapeRealTimeData;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : Event { }
