using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleCodeConverter()
    : ValueConverter<RoleCode, string>(id => id.Value, value => new RoleCode(value)) { }
