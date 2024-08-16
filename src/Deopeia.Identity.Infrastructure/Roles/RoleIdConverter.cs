using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleIdConverter()
    : ValueConverter<RoleId, Guid>(id => id.Guid, guid => new RoleId(guid)) { }
