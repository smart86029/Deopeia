using Deopeia.Quote.Domain.Candles;

namespace Deopeia.Quote.Application.Candles.CalculateCandles;

public class CalculateCandlesCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    ICandleRepository candleRepository
) : IEventHandler<PriceChangedEvent>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICandleRepository _candleRepository = candleRepository;

    public async Task Handle(PriceChangedEvent @event)
    {
        var symbol = new Symbol(@event.Symbol);
        var timeFrame = TimeFrame.Intraday;
        var timestamp = DateTimeOffset.UtcNow.Midnight();
        var candle = await _candleRepository.GetCandleAsync(
            new CandleId(symbol, timeFrame, timestamp)
        );
        if (candle is null)
        {
            candle = new Candle(symbol, timeFrame, timestamp);
            await _candleRepository.AddAsync(candle);
        }

        var ticks = new[]
        {
            new Tick(@event.LastTradedAt, @event.LastTradedPrice, @event.LastTradedPrice),
        };
        candle.Calculate(ticks);

        await _unitOfWork.CommitAsync();
    }
}
