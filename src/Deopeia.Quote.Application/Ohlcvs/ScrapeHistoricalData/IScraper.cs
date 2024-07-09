namespace Deopeia.Quote.Application.Ohlcvs.ScrapeHistoricalData;

public interface IScraper
{
    Task<ICollection<OhlcvDto>> GetOhlcvsAsync(DateOnly date);
}
