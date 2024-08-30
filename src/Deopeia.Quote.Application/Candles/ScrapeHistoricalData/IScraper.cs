namespace Deopeia.Quote.Application.Candles.ScrapeHistoricalData;

public interface IScraper
{
    Task<ICollection<CandleDto>> GetOhlcvsAsync(DateOnly date);
}
