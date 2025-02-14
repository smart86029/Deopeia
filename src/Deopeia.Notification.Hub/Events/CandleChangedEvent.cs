namespace Deopeia.Notification.Hub.Events;

public record CandleChangedEvent(
    string Symbol,
    int TimeFrame,
    DateTimeOffset Timestamp,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) : Event { }
