namespace Deopeia.Quote.Domain.Candles;

public class Candle : AggregateRoot<CandleId>
{
    private static readonly Dictionary<TimeFrame, TimeSpan> Intervals = new()
    {
        [TimeFrame.M1] = TimeSpan.FromMinutes(1),
        [TimeFrame.M5] = TimeSpan.FromMinutes(5),
        [TimeFrame.M15] = TimeSpan.FromMinutes(15),
        [TimeFrame.M30] = TimeSpan.FromMinutes(30),
        [TimeFrame.H1] = TimeSpan.FromHours(1),
        [TimeFrame.H4] = TimeSpan.FromHours(4),
        [TimeFrame.D1] = TimeSpan.FromDays(1),
        [TimeFrame.W1] = TimeSpan.FromDays(7),
        [TimeFrame.MN] = TimeSpan.FromDays(30),
    };

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

    public void Calculate(Tick tick)
    {
        var max = Timestamp.Add(Intervals[TimeFrame]);
        if (!tick.Timestamp.IsBetween(Timestamp, max))
        {
            return;
        }

        Open = Open != 0 && tick.Timestamp >= Timestamp ? Open : tick.Price;
        High = Math.Max(High, tick.Price);
        Low = Low == 0 ? tick.Price : Math.Min(Low, tick.Price);
        Close = Close != 0 && Timestamp >= tick.Timestamp ? Close : tick.Price;
        Volume += tick.Volume;
    }

    public void Calculate(IEnumerable<Tick> ticks)
    {
        if (!ticks.Any())
        {
            return;
        }

        Open = Open > 0 ? Open : ticks.First().Price;
        High = Math.Max(High, ticks.Max(x => x.Price));
        Low = Low == 0 ? ticks.Min(x => x.Price) : Math.Min(Low, ticks.Min(x => x.Price));
        Close = ticks.Last().Price;
        Volume += ticks.Sum(x => x.Volume);
    }
}
