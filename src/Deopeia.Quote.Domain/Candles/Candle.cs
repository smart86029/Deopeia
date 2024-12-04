namespace Deopeia.Quote.Domain.Candles;

public class Candle : AggregateRoot<CandleId>
{
    public Candle(
        Symbol symbol,
        TimeFrame timeFrame,
        DateTimeOffset timestamp,
        decimal open,
        decimal high,
        decimal low,
        decimal close,
        decimal volume
    )
        : base(new CandleId(symbol, timeFrame, timestamp))
    {
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }

    public Symbol Symbol => Id.Symbol;

    public TimeFrame TimeFrame => Id.TimeFrame;

    public DateTimeOffset Timestamp => Id.Timestamp;

    public decimal Open { get; private init; }

    public decimal High { get; private init; }

    public decimal Low { get; private init; }

    public decimal Close { get; private init; }

    public decimal Volume { get; private init; }
}
