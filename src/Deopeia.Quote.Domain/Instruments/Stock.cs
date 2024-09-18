using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Domain.Instruments;

public class Stock : Instrument
{
    private Stock() { }

    public Stock(ExchangeId exchangeId, string symbol, string name, CompanyId companyId)
        : base(MarketType.Stock, exchangeId, symbol, name)
    {
        CompanyId = companyId;
    }

    public CompanyId CompanyId { get; private init; }
}
