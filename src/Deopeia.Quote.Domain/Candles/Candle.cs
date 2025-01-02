namespace Deopeia.Quote.Domain.Candles;

public class Candle : AggregateRoot<CandleId>
{
    public static readonly Dictionary<TimeFrame, TimeSpan> Intervals = new()
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
        : this(new CandleId(symbol, timeFrame, timestamp)) { }

    public Candle(CandleId id)
        : base(id) { }

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
        var sorted = ticks.Where(x => x.Symbol == Symbol && IsInRange(x)).OrderBy(x => x.Timestamp);
        if (!sorted.Any())
        {
            return;
        }

        Open = Open > 0 ? Open : sorted.First().Price;
        High = Math.Max(High, sorted.Max(x => x.Price));
        Low = Low == 0 ? sorted.Min(x => x.Price) : Math.Min(Low, sorted.Min(x => x.Price));
        Close = sorted.Last().Price;
        Volume += sorted.Sum(x => x.Volume);
    }

    private bool IsInRange(Tick tick)
    {
        var max =
            TimeFrame == TimeFrame.MN
                ? Timestamp.AddMonths(1)
                : Timestamp.Add(Intervals[TimeFrame]);

        return tick.Timestamp >= Timestamp && tick.Timestamp < max;
    }
}
