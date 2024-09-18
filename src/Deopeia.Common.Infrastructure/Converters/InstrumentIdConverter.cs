using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Converters;

internal class InstrumentIdConverter()
    : ValueConverter<InstrumentId, Guid>(id => id.Guid, guid => new InstrumentId(guid)) { }
