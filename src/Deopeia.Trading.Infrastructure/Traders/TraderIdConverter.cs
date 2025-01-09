using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TraderIdConverter()
    : ValueConverter<TraderId, Guid>(id => id.Guid, guid => new TraderId(guid)) { }
