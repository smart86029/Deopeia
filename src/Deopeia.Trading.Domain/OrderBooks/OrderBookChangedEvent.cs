namespace Deopeia.Trading.Domain.OrderBooks;

public record OrderBookChangedEvent(string Symbol, OrderDto[] Bids, OrderDto[] Asks)
    : DomainEvent { }
