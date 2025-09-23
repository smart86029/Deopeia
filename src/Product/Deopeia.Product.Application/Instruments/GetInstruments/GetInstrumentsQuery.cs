using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Application.Instruments.GetInstruments;

public sealed record GetInstrumentsQuery(string? Keyword, InstrumentType? Type)
    : PagedQuery<InstrumentDto>;
