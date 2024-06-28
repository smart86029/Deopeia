namespace Deopeia.Quote.Application.Ohlcvs.GetHistoricalData;

public class GetHistoricalDataViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public List<OhlcvDto> Quotes { get; set; } = [];
}
