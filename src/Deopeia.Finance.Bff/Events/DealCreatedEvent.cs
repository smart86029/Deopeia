namespace Deopeia.Finance.Bff.Events;

public record DealCreatedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : Event { }
