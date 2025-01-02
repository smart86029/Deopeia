namespace Deopeia.Trading.Domain.OrderBooks;

public record DealCreatedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : DomainEvent { }
