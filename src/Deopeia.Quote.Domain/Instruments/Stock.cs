using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Domain.Instruments;

public class Stock : Instrument
{
    private Stock() { }

    public Stock(string symbol, CompanyId companyId)
        : base(MarketType.Stock, symbol)
    {
        CompanyId = companyId;
    }

    public CompanyId CompanyId { get; init; }
}
