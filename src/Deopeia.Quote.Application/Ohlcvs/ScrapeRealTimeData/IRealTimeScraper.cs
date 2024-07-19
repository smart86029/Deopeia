namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

public interface IRealTimeScraper
{
    Task<ICollection<OhlcvDto>> GetOhlcvsAsync(IEnumerable<string> symbols);
}
