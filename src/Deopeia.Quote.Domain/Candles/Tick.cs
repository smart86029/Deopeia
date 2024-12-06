namespace Deopeia.Quote.Domain.Candles;

public record Tick(DateTimeOffset Timestamp, decimal Price, decimal Volume) { }
