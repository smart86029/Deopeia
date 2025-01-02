using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.CalculateCandles;

public class CalculateCandlesCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    ICandleRepository candleRepository,
    IEventBus eventBus
) : IRequestHandler<CalculateCandlesCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandleRepository _candleRepository = candleRepository;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(CalculateCandlesCommand request, CancellationToken cancellationToken)
    {
        var timeFrame = request.TimeFrame;
        var (from, to) = GetRange(timeFrame, DateTimeOffset.UtcNow);
        if (timeFrame == TimeFrame.M1)
        {
            var ticks = await _candleRepository.GetTicksAsync(from, to);
            foreach (var group in ticks.GroupBy(t => t.Symbol))
            {
                var symbol = group.Key;
                var id = new CandleId(symbol, timeFrame, from);
                var candle = await _candleRepository.GetCandleAsync(id);
                if (candle is null)
                {
                    candle = new Candle(id);
                    await _candleRepository.AddAsync(candle);
                }
                candle.Calculate(ticks);
                await _unitOfWork.CommitAsync();
                await _eventBus.PublishAsync(
                    new CandleChangedEvent(
                        symbol.Value,
                        (int)timeFrame,
                        from,
                        candle.Open,
                        candle.High,
                        candle.Low,
                        candle.Close,
                        candle.Volume
                    )
                );
            }
        }
    }

    public static (DateTimeOffset From, DateTimeOffset To) GetRange(
        TimeFrame timeFrame,
        DateTimeOffset timestamp
    )
    {
        var interval = Candle.Intervals[timeFrame];
        DateTimeOffset temp;
        DateTimeOffset from;
        switch (timeFrame)
        {
            case TimeFrame.M1:
            case TimeFrame.M5:
            case TimeFrame.M15:
            case TimeFrame.M30:
                var minutes = interval.TotalMinutes;
                temp = timestamp.AddMinutes(-(timestamp.Minute % minutes) - minutes);
                from = new DateTimeOffset(
                    temp.Year,
                    temp.Month,
                    temp.Day,
                    temp.Hour,
                    temp.Minute,
                    0,
                    temp.Offset
                );
                return (from, from.Add(interval));

            case TimeFrame.H1:
            case TimeFrame.H4:
                var hours = interval.TotalHours;
                temp = timestamp.AddHours(-(timestamp.Hour % hours) - hours);
                from = new DateTimeOffset(
                    temp.Year,
                    temp.Month,
                    temp.Day,
                    temp.Hour,
                    0,
                    0,
                    temp.Offset
                );
                return (from, from.Add(interval));

            case TimeFrame.D1:
                var days = interval.TotalDays;
                temp = timestamp.AddDays(-1);
                from = new DateTimeOffset(temp.Year, temp.Month, temp.Day, 0, 0, 0, temp.Offset);
                return (from, from.Add(interval));

            case TimeFrame.W1:
                var daysToMonday = timestamp.DayOfWeek - DayOfWeek.Monday + interval.Days;
                temp = timestamp.AddDays(-(daysToMonday % interval.Days) - interval.Days);
                from = new DateTimeOffset(temp.Year, temp.Month, temp.Day, 0, 0, 0, temp.Offset);
                return (from, from.Add(interval));

            case TimeFrame.MN:
                temp = timestamp.AddMonths(-1);
                from = new DateTimeOffset(temp.Year, temp.Month, 1, 0, 0, 0, temp.Offset);
                return (from, from.AddMonths(1));

            default:
                throw new ArgumentException("Invalid TimeFrame");
        }
    }
}
