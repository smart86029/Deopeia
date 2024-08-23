using Deopeia.Quote.Domain.Exchanges;

namespace Deopeia.Quote.Infrastructure.Exchanges;

internal class ExchangeIdConverter()
    : ValueConverter<ExchangeId, Guid>(id => id.Guid, guid => new ExchangeId(guid)) { }
