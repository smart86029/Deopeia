using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Domain.Permissions;

public interface IPermissionRepository : IRepository<Permission, PermissionCode>
{
    Task<ICollection<Permission>> GetPermissionsAsync();

    Task<ICollection<Permission>> GetPermissionsAsync(IEnumerable<PermissionCode> permissionCodes);

    Task<ICollection<PermissionCode>> GetPermissionCodesAsync(UserId userId);

    Task<Permission> GetPermissionAsync(PermissionCode permissionCode);

    void Add(Permission permission);

    void Remove(Permission permission);
}
