namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

public class RealTimeDto
{
    public string Symbol { get; set; } = string.Empty;

    public DateTimeOffset LastTradedAt { get; set; }

    public decimal LastTradedPrice { get; set; }

    public decimal PreviousClose { get; set; }
}
