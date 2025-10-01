namespace Deopeia.AdminPortal.Bff.Models.Instruments;

public sealed record UpdateRequest(
    Guid Id,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    PriceConstraints PriceConstraints,
    QuantityConstraints QuantityConstraints,
    IReadOnlyList<InstrumentLocalization> Localizations
);
