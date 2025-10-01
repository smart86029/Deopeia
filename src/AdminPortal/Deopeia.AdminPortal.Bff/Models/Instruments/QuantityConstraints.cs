namespace Deopeia.AdminPortal.Bff.Models.Instruments;

public sealed record QuantityConstraints(
    decimal StepSize,
    decimal MinQuantity,
    decimal MinNotional
);
