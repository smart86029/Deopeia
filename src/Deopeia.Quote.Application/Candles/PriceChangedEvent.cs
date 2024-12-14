namespace Deopeia.Quote.Application.Candles;

public record PriceChangedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : Event { }
