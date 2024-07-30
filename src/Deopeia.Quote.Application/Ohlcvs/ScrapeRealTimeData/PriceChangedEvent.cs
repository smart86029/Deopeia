namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : Event { }
