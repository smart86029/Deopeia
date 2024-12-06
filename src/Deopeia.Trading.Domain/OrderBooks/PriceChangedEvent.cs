namespace Deopeia.Trading.Domain.OrderBooks;

public record PriceChangedEvent(
    string Symbol,
    DateTimeOffset LastTradedAt,
    decimal LastTradedPrice,
    decimal PreviousClose
) : DomainEvent { }
