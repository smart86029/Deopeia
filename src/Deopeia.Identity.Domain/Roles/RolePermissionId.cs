using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Domain.Roles;

public readonly record struct RolePermissionId(RoleId RoleId, PermissionId PermissionId)
    : IEntityId { }
