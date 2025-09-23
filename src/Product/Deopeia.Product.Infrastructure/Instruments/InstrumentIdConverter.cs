using Deopeia.Product.Domain.Instruments;

namespace Deopeia.Product.Infrastructure.Instruments;

internal sealed class InstrumentIdConverter()
    : ValueConverter<InstrumentId, Guid>(id => id.Guid, guid => new InstrumentId(guid)) { }
