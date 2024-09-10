namespace Deopeia.Identity.Domain.Permissions;

public interface IPermissionRepository : IRepository<Permission, PermissionId>
{
    Task<ICollection<Permission>> GetPermissionsAsync();

    Task<ICollection<Permission>> GetPermissionsAsync(IEnumerable<PermissionId> permissionIds);

    Task<Permission> GetPermissionAsync(PermissionId permissionId);

    void Add(Permission permission);

    void Remove(Permission permission);
}
