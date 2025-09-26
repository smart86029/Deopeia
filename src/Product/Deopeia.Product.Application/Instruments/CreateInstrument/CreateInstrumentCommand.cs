using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Application.Instruments.CreateInstrument;

public sealed record CreateInstrumentCommand(
    InstrumentType Type,
    string Symbol,
    string BaseAsset,
    string QuoteAsset,
    int PricePrecision,
    int QuantityPrecision,
    decimal MinQuantity,
    decimal MinNotional,
    IEnumerable<InstrumentLocalizationDto> Localizations
) : ICommand;
