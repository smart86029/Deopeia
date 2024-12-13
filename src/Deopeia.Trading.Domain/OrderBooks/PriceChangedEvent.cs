namespace Deopeia.Trading.Domain.OrderBooks;

public record PriceChangedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : DomainEvent { }
