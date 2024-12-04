namespace Deopeia.Quote.Domain.Candles;

public class Candle(
    Symbol instrumentId,
    TimeFrame timeFrame,
    DateTimeOffset timestamp,
    decimal open,
    decimal high,
    decimal low,
    decimal close,
    decimal volume
) : AggregateRoot<CandleId>(new CandleId(instrumentId, timeFrame, timestamp))
{
    public Symbol InstrumentId => Id.InstrumentId;

    public TimeFrame TimeFrame => Id.TimeFrame;

    public DateTimeOffset Timestamp => Id.Timestamp;

    public decimal Open { get; private init; } = open;

    public decimal High { get; private init; } = high;

    public decimal Low { get; private init; } = low;

    public decimal Close { get; private init; } = close;

    public decimal Volume { get; private init; } = volume;
}
