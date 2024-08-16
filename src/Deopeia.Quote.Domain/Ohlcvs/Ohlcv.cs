namespace Deopeia.Quote.Domain.Ohlcvs;

public class Ohlcv(
    string symbol,
    DateTimeOffset recordedAt,
    decimal open,
    decimal high,
    decimal low,
    decimal close,
    decimal volume
) : AggregateRoot<OhlcvId>(new OhlcvId(symbol, recordedAt))
{
    public string Symbol => Id.Symbol;

    public DateTimeOffset RecordedAt => Id.RecordedAt;

    public decimal Open { get; private init; } = open;

    public decimal High { get; private init; } = high;

    public decimal Low { get; private init; } = low;

    public decimal Close { get; private init; } = close;

    public decimal Volume { get; private init; } = volume;
}
