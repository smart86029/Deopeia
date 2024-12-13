namespace Deopeia.Finance.Bff.Events;

public record PriceChangedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : Event { }
