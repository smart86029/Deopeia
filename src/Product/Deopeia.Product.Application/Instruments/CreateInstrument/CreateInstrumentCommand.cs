using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Application.Instruments.CreateInstrument;

public sealed record CreateInstrumentCommand(
    InstrumentType Type,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    PriceConstraintsDto PriceConstraints,
    QuantityConstraintsDto QuantityConstraints,
    IEnumerable<InstrumentLocalizationDto> Localizations
) : ICommand;
