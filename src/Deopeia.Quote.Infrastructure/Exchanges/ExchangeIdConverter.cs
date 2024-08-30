using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges;

internal class ExchangeIdConverter()
    : ValueConverter<ExchangeId, string>(id => id.Mic, mic => new ExchangeId(mic)) { }
