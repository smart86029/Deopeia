namespace Deopeia.Quote.Domain.Candles;

public readonly record struct CandleId(Symbol Symbol, TimeFrame TimeFrame, DateTimeOffset Timestamp)
    : IEntityId { }
