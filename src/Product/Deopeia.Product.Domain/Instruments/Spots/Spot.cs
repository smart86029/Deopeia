namespace Deopeia.Product.Domain.Instruments.Spots;

public class Spot : Instrument
{
    private Spot() { }

    public Spot(
        Symbol symbol,
        string name,
        string baseAsset,
        string quoteAsset,
        int pricePrecision,
        int quantityPrecision,
        decimal minQuantity,
        decimal minNotional
    )
        : base(
            InstrumentType.Spot,
            symbol,
            name,
            baseAsset,
            quoteAsset,
            pricePrecision,
            quantityPrecision,
            minQuantity,
            minNotional
        ) { }
}
