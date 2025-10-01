namespace Deopeia.Product.Domain.Instruments;

public readonly record struct PriceConstraints
{
    public PriceConstraints(decimal tickSize)
    {
        tickSize.MustGreaterThan(0);

        TickSize = tickSize;
    }

    public decimal TickSize { get; private init; }
}
