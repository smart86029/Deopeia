using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionIdConverter()
    : ValueConverter<PermissionId, Guid>(id => id.Guid, guid => new PermissionId(guid)) { }
