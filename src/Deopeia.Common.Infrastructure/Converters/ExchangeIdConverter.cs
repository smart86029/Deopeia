using Deopeia.Common.Domain.Finance;

namespace Deopeia.Common.Infrastructure.Converters;

internal class ExchangeIdConverter()
    : ValueConverter<ExchangeId, string>(id => id.Mic, mic => new ExchangeId(mic)) { }