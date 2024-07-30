namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

public interface IRealTimeScraper
{
    Task<ICollection<RealTimeDto>> GetRealTimeDataAsync(IEnumerable<string> symbols);
}
