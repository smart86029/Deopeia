namespace Deopeia.Quote.Domain.Instruments.FuturesContracts;

public class FuturesContract : Instrument
{
    private FuturesContract() { }

    public FuturesContract(
        ExchangeId exchangeId,
        string symbol,
        string name,
        Currency currency,
        AssetId underlyingAssetId,
        ContractSize contractSize,
        decimal tickSize
    )
        : base(InstrumentType.Futures, exchangeId, symbol, name, currency)
    {
        UnderlyingAssetId = underlyingAssetId;
        ContractSize = contractSize;
        TickSize = tickSize;
    }

    public AssetId UnderlyingAssetId { get; private init; }

    public ContractSize ContractSize { get; private init; }

    public decimal TickSize { get; private init; }
}
