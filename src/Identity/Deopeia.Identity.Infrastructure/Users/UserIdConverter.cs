using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal sealed class UserIdConverter()
    : ValueConverter<UserId, Guid>(id => id.Guid, guid => new UserId(guid)) { }
