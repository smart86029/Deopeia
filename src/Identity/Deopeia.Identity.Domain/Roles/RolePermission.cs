using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Domain.Roles;

public record RolePermission(RoleCode RoleCode, PermissionCode PermissionCode) : ValueObject { }
