using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentIdConverter()
    : ValueConverter<InstrumentId, Guid>(id => id.Guid, guid => new InstrumentId(guid)) { }
