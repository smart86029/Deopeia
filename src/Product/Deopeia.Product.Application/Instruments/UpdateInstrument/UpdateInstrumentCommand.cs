namespace Deopeia.Product.Application.Instruments.UpdateInstrument;

public sealed record UpdateInstrumentCommand(
    Guid Id,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    PriceConstraintsDto PriceConstraints,
    QuantityConstraintsDto QuantityConstraints,
    IEnumerable<InstrumentLocalizationDto> Localizations
) : ICommand;
