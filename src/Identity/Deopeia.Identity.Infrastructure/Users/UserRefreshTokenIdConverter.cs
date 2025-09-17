using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Users;

internal sealed class UserRefreshTokenIdConverter()
    : ValueConverter<UserRefreshTokenId, Guid>(
        id => id.Guid,
        guid => new UserRefreshTokenId(guid)
    ) { }
