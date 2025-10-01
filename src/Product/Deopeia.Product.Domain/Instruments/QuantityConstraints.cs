namespace Deopeia.Product.Domain.Instruments;

public readonly record struct QuantityConstraints
{
    public QuantityConstraints(decimal stepSize, decimal minQuantity, decimal minNotional)
    {
        stepSize.MustGreaterThan(0);
        minQuantity.MustGreaterThan(0);
        minNotional.MustGreaterThan(0);

        if (!IsOnStep(minQuantity, stepSize))
        {
            throw new ArgumentException(
                $"MinQuantity {minQuantity} is not on step {stepSize}.",
                nameof(minQuantity)
            );
        }

        StepSize = stepSize;
        MinQuantity = minQuantity;
        MinNotional = minNotional;
    }

    public decimal StepSize { get; private init; }

    public decimal MinQuantity { get; private init; }

    public decimal MinNotional { get; private init; }

    private static bool IsOnStep(decimal value, decimal stepSize)
    {
        return value % stepSize == 0;
    }
}
