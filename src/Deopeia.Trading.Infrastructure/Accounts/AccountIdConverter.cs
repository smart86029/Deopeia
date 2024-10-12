using Deopeia.Trading.Domain.Accounts;

namespace Deopeia.Trading.Infrastructure.Accounts;

internal class AccountIdConverter()
    : ValueConverter<AccountId, Guid>(id => id.Guid, guid => new AccountId(guid)) { }
