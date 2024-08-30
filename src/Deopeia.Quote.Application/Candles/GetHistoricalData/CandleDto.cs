namespace Deopeia.Quote.Application.Candles.GetHistoricalData;

public class CandleDto
{
    public DateTimeOffset Date { get; set; }

    public decimal Open { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }

    public decimal Close { get; set; }

    public decimal Volume { get; set; }
}
