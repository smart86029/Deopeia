using Deopeia.Common.Domain.Finance;

namespace Deopeia.Quote.Infrastructure.Instruments;

internal class InstrumentIdConverter()
    : ValueConverter<InstrumentId, Guid>(id => id.Guid, guid => new InstrumentId(guid)) { }
