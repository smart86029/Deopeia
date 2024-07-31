using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

internal class ScrapeRealTimeDataCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IOhlcvRepository ohlcvRepository,
    IEventBus eventBus,
    IRealTimeScraper scraper
) : IRequestHandler<ScrapeRealTimeDataCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOhlcvRepository _ohlcvRepository = ohlcvRepository;
    private readonly IEventBus _eventBus = eventBus;
    private readonly IRealTimeScraper _scraper = scraper;

    public async Task Handle(ScrapeRealTimeDataCommand request, CancellationToken cancellationToken)
    {
        //if (DateTime.UtcNow.TimeOfDay > new TimeSpan(5, 30, 0))
        //{
        //    return;
        //}

        var symbols = new[] { "2330", "0050", "2357" };
        var items = await _scraper.GetRealTimeDataAsync(symbols);
        var ohlcvs = items.Chunk(1000);

        foreach (var chunk in ohlcvs)
        {
            //await _ohlcvRepository.AddAsync(chunk);
            //await _unitOfWork.CommitAsync();

            var events = chunk
                .Select(x => new PriceChangedEvent(
                    x.Symbol,
                    x.LastTradedAt,
                    x.LastTradedPrice,
                    x.PreviousClose
                ))
                .Where(x => x.LastTradedPrice > 0)
                .ToList();
            foreach (var @event in events)
            {
                await _eventBus.PublishAsync(@event);
            }
        }
    }
}
