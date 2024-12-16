using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.CalculateCandles;

public class CalculateCandlesEventHandler(
    IQuoteUnitOfWork unitOfWork,
    ICandleRepository candleRepository,
    IEventBus eventBus
) : IEventHandler<PriceChangedEvent>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandleRepository _candleRepository = candleRepository;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(PriceChangedEvent @event)
    {
        var symbol = new Symbol(@event.Symbol);
        var timeFrame = TimeFrame.M1;
        var timestamp = GetTimestamp(timeFrame, @event.CreatedAt);
        var id = new CandleId(symbol, timeFrame, timestamp);
        var candle = await _candleRepository.GetCandleAsync(id);
        if (candle is null)
        {
            candle = new Candle(symbol, timeFrame, timestamp);
            await _candleRepository.AddAsync(candle);
        }

        var ticks = new[] { new Tick(@event.CreatedAt, @event.Price, @event.Volume) };
        candle.Calculate(ticks);

        await _unitOfWork.CommitAsync();

        await _eventBus.PublishAsync(
            new CandleChangedEvent(
                @event.Symbol,
                (int)timeFrame,
                timestamp,
                candle.Open,
                candle.High,
                candle.Low,
                candle.Close,
                candle.Volume
            )
        );
    }

    private DateTimeOffset GetTimestamp(TimeFrame timeFrame, DateTimeOffset now)
    {
        return new DateTimeOffset(
            now.Year,
            now.Month,
            now.Day,
            now.Hour,
            now.Minute,
            0,
            now.Offset
        );
    }
}
