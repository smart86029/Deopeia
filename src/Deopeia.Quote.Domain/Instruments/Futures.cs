namespace Deopeia.Quote.Domain.Instruments;

public class Futures : Instrument
{
    private Futures() { }

    public Futures(ExchangeId exchangeId, string symbol, string name, Currency currency)
        : base(InstrumentType.Futures, exchangeId, symbol, name, currency) { }

    public AssetId UnderlyingAssetId { get; private init; }

    public ContractSize ContractSize { get; private init; }

    public decimal TickSize { get; private init; }
}
