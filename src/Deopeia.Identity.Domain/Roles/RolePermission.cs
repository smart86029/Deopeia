using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Domain.Roles;

public record RolePermission(RoleId RoleId, PermissionId PermissionId) : ValueObject { }
