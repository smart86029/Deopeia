namespace Deopeia.Product.Application.Instruments;

public sealed record QuantityConstraintsDto(
    decimal StepSize,
    decimal MinQuantity,
    decimal MinNotional
);
