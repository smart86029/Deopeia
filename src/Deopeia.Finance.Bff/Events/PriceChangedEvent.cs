namespace Deopeia.Finance.Bff.Events;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : Event { }
