using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Domain.Roles;

public class RolePermission : Entity<RolePermissionId>
{
    //private RolePermission() { }

    public RolePermission(RoleId roleId, PermissionId permissionId)
        : base(new RolePermissionId(roleId, permissionId)) { }

    public RoleId RoleId => Id.RoleId;

    public PermissionId PermissionId => Id.PermissionId;
}
