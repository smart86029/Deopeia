namespace Deopeia.Product.Domain.Instruments.Spots;

public class Spot : Instrument
{
    private Spot() { }

    public Spot(
        Symbol symbol,
        string name,
        string baseAsset,
        string quoteAsset,
        PriceConstraints priceConstraints,
        QuantityConstraints orderConstraints
    )
        : base(
            InstrumentType.Spot,
            symbol,
            name,
            baseAsset,
            quoteAsset,
            priceConstraints,
            orderConstraints
        ) { }
}
