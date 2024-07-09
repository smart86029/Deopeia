using Deopeia.Quote.Domain.Ohlcvs;

namespace Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;

internal class ScrapeHistoricalDataCommandHandler(
    IQuoteUnitOfWork unitOfWork,
    IOhlcvRepository ohlcvRepository,
    IScraper scraper
) : IRequestHandler<ScrapeHistoricalDataCommand>
{
    private readonly IQuoteUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOhlcvRepository _ohlcvRepository = ohlcvRepository;
    private readonly IScraper _scraper = scraper;

    public async Task Handle(
        ScrapeHistoricalDataCommand request,
        CancellationToken cancellationToken
    )
    {
        var exists = await _ohlcvRepository.ExistsAsync(request.Date);
        if (exists)
        {
            return;
        }

        var items = await _scraper.GetOhlcvsAsync(request.Date);
        var ohlcvs = items
            .Select(x => new Ohlcv(
                x.Symbol,
                x.Date.ToDateTimeOffset(),
                x.Open,
                x.High,
                x.Low,
                x.Close,
                x.Volume
            ))
            .Chunk(1000);

        foreach (var chunk in ohlcvs)
        {
            await _ohlcvRepository.AddAsync(chunk);
            await _unitOfWork.CommitAsync();
        }
    }
}
