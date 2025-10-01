namespace Deopeia.AdminPortal.Bff.Models.Instruments;

public sealed record InstrumentResponse(
    Guid Id,
    int Type,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    PriceConstraints PriceConstraints,
    QuantityConstraints QuantityConstraints,
    IReadOnlyList<InstrumentLocalization> Localizations
);
