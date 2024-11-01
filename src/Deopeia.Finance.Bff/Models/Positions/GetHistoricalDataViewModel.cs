namespace Deopeia.Finance.Bff.Models.Positions;

public class GetHistoricalDataViewModel
{
    public string Symbol { get; set; } = string.Empty;

    public List<CandleDto> Quotes { get; set; } = [];
}
