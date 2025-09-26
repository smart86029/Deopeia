namespace Deopeia.Product.Application.Instruments.UpdateInstrument;

public sealed record UpdateInstrumentCommand(
    Guid Id,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    int PricePrecision,
    int QuantityPrecision,
    decimal MinQuantity,
    decimal MinNotional,
    IEnumerable<InstrumentLocalizationDto> Localizations
) : ICommand;
