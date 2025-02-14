namespace Deopeia.Notification.Hub.RealTime;

public record Candle(
    DateTimeOffset Timestamp,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) { }
