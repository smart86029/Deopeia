namespace Deopeia.Quote.Domain.Candles;

public record Tick(Symbol Symbol, DateTimeOffset Timestamp, decimal Price, decimal Volume) { }
