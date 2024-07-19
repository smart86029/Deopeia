namespace Deopeia.Quote.Application.Ohlcvs.ScrapeRealTimeData;

public class OhlcvDto
{
    public string Symbol { get; set; } = string.Empty;

    public DateTimeOffset DateTime { get; set; }

    public decimal Open { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Close { get; set; }

    public decimal Volume { get; set; }
}
