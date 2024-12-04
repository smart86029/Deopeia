namespace Deopeia.Quote.Domain.Candles;

public readonly record struct CandleId(
    Symbol InstrumentId,
    TimeFrame TimeFrame,
    DateTimeOffset Timestamp
) : IEntityId { }
