namespace Deopeia.Quote.Application.Candles.DealCreated;

public record DealCreatedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : Event { }
