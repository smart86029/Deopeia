namespace Deopeia.Finance.Bff.Models.RealTime;

public record Candle(
    int TimeFrame,
    long Timestamp,
    decimal Open,
    decimal High,
    decimal Low,
    decimal Close,
    decimal Volume
) { }
