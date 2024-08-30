namespace Deopeia.Quote.Application.Candles.GetHistoricalData;

public class GetHistoricalDataViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public List<CandleDto> Quotes { get; set; } = [];
}
