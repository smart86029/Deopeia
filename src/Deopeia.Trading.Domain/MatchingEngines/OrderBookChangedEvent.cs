namespace Deopeia.Trading.Domain.MatchingEngines;

public record OrderBookChangedEvent(string Symbol, OrderDto[] Bids, OrderDto[] Asks)
    : DomainEvent { }
