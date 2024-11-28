namespace Deopeia.Trading.Domain.MatchingEngines;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : DomainEvent { }
