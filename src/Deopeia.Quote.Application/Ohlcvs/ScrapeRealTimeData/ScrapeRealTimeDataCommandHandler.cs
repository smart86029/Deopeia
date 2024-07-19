using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

internal class ScrapeRealTimeDataCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IOhlcvRepository ohlcvRepository,
    IRealTimeScraper scraper
) : IRequestHandler<ScrapeRealTimeDataCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOhlcvRepository _ohlcvRepository = ohlcvRepository;
    private readonly IRealTimeScraper _scraper = scraper;

    public async Task Handle(ScrapeRealTimeDataCommand request, CancellationToken cancellationToken)
    {
        if (DateTime.UtcNow.TimeOfDay > new TimeSpan(5, 30, 0))
        {
            return;
        }

        var symbols = new[] { "2330", "0050", "2357" };
        var items = await _scraper.GetOhlcvsAsync(symbols);
        var ohlcvs = items
            .Select(x => new Ohlcv(x.Symbol, x.DateTime, x.Open, x.High, x.Low, x.Close, x.Volume))
            .Chunk(1000);

        foreach (var chunk in ohlcvs)
        {
            await _ohlcvRepository.AddAsync(chunk);
            await _unitOfWork.CommitAsync();
        }
    }
}
