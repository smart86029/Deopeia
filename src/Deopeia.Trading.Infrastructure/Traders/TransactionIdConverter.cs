using Deopeia.Trading.Domain.Traders;

namespace Deopeia.Trading.Infrastructure.Traders;

internal class TransactionIdConverter()
    : ValueConverter<TransactionId, Guid>(id => id.Guid, guid => new TransactionId(guid)) { }
