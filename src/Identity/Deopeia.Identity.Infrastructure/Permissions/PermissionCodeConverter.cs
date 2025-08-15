using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionCodeConverter()
    : ValueConverter<PermissionCode, string>(id => id.Value, value => new PermissionCode(value)) { }
