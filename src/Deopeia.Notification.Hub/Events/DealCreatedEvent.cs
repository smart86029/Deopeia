namespace Deopeia.Notification.Hub.Events;

public record DealCreatedEvent(
    string Symbol,
    decimal Price,
    decimal Volume,
    decimal Bid,
    decimal Ask
) : Event { }
