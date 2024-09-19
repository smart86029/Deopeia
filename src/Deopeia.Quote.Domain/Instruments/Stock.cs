using Deopeia.Quote.Domain.Companies;

namespace Deopeia.Quote.Domain.Instruments;

public class Stock : Instrument
{
    private Stock() { }

    public Stock(
        ExchangeId exchangeId,
        string symbol,
        string name,
        Currency currency,
        CompanyId companyId
    )
        : base(InstrumentType.Stock, exchangeId, symbol, name, currency)
    {
        CompanyId = companyId;
    }

    public CompanyId CompanyId { get; private init; }
}
