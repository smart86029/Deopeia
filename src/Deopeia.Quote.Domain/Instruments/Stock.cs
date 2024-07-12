namespace Deopeia.Quote.Domain.Instruments;

public class Stock : Instrument
{
    private Stock() { }

    public Stock(string symbol, Guid companyId)
        : base(MarketType.Stock, symbol)
    {
        CompanyId = companyId;
    }

    public Guid CompanyId { get; init; }
}
