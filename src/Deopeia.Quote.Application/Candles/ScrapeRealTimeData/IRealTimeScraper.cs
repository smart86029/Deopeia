namespace Deopeia.Quote.Application.Candles.ScrapeRealTimeData;

public interface IRealTimeScraper
{
    Task<ICollection<RealTimeDto>> GetRealTimeDataAsync(IEnumerable<string> symbols);
}
