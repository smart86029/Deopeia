using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Application.Stocks.GetStocks;

public class StockDto
{
    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public decimal PriceChange { get; set; }

    public decimal Volume { get; set; }

    public decimal PriceToEarningsRatio { get; set; }

    public decimal PriceBookRatio { get; set; }

    public decimal DividendYield { get; set; }

    [JsonIgnore]
    public SubIndustry SubIndustry { get; set; }

    public string Industry { get; set; } = string.Empty;
}
