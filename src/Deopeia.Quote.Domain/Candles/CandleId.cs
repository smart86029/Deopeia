namespace Deopeia.Quote.Domain.Candles;

public readonly record struct CandleId(
    InstrumentId InstrumentId,
    TimeFrame TimeFrame,
    DateTimeOffset Timestamp
) : IEntityId { }
