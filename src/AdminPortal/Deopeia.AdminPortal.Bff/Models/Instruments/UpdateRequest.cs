namespace Deopeia.AdminPortal.Bff.Models.Instruments;

public sealed record UpdateRequest(
    Guid Id,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    int PricePrecision,
    int QuantityPrecision,
    decimal MinQuantity,
    decimal MinNotional,
    IReadOnlyList<InstrumentLocalization> Localizations
);
