using Deopeia.Quote.Domain.Candles;
using Deopeia.Quote.Domain.Exchanges;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Application.Candles.ScrapeHistoricalData;

internal class ScrapeHistoricalDataCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IInstrumentRepository instrumentRepository,
    ICandleRepository candleRepository,
    IScraper scraper
) : IRequestHandler<ScrapeHistoricalDataCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IInstrumentRepository _instrumentRepository = instrumentRepository;
    private readonly ICandleRepository _candleRepository = candleRepository;
    private readonly IScraper _scraper = scraper;

    public async Task Handle(
        ScrapeHistoricalDataCommand request,
        CancellationToken cancellationToken
    )
    {
        var exists = await _candleRepository.ExistsAsync(request.Date);
        if (exists)
        {
            return;
        }

        var map = await _instrumentRepository.GetSymbolMap(new ExchangeId("XTAI"));
        var items = await _scraper.GetOhlcvsAsync(request.Date);
        var candles = items
            .Select(x => new Candle(
                map[x.Symbol],
                TimeFrame.Intraday,
                x.Date.ToDateTimeOffset(),
                x.Open,
                x.High,
                x.Low,
                x.Close,
                x.Volume
            ))
            .Chunk(1000);

        foreach (var chunk in candles)
        {
            await _candleRepository.AddAsync(chunk);
            await _unitOfWork.CommitAsync();
        }
    }
}
