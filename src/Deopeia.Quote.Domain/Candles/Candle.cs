namespace Deopeia.Quote.Domain.Candles;

public class Candle : AggregateRoot<CandleId>
{
    public Candle(Symbol symbol, TimeFrame timeFrame, DateTimeOffset timestamp)
        : base(new CandleId(symbol, timeFrame, timestamp)) { }

    public Symbol Symbol => Id.Symbol;

    public TimeFrame TimeFrame => Id.TimeFrame;

    public DateTimeOffset Timestamp => Id.Timestamp;

    public decimal Open { get; private set; }

    public decimal High { get; private set; }

    public decimal Low { get; private set; }

    public decimal Close { get; private set; }

    public decimal Volume { get; private set; }

    public void Calculate(IEnumerable<Tick> ticks)
    {
        if (!ticks.Any())
        {
            return;
        }

        Open = Open > 0 ? Open : ticks.First().Price;
        High = Math.Max(High, ticks.Max(x => x.Price));
        Low = Math.Min(Math.Max(0, Low), ticks.Min(x => x.Price));
        Close = ticks.Last().Price;
        Volume += ticks.Sum(x => x.Volume);
    }
}
