namespace Deopeia.Finance.Bff.Models.RealTime;

public record Candle(
    DateTimeOffset Timestamp,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) { }
