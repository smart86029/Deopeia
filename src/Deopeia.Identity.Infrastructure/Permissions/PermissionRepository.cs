using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionRepository(IdentityContext context) : IPermissionRepository
{
    private readonly DbSet<Permission> _permissions = context.Set<Permission>();

    public async Task<ICollection<Permission>> GetPermissionsAsync()
    {
        var results = await _permissions.Include(x => x.Locales).ToListAsync();

        return results;
    }

    public async Task<ICollection<Permission>> GetPermissionsAsync(
        IEnumerable<PermissionId> permissionIds
    )
    {
        var results = await _permissions
            .Include(x => x.Locales)
            .Where(x => permissionIds.Contains(x.Id))
            .ToListAsync();

        return results;
    }

    public async Task<Permission> GetPermissionAsync(PermissionId permissionId)
    {
        var result = await _permissions
            .Include(x => x.Locales)
            .SingleAsync(x => x.Id == permissionId);

        return result;
    }

    public void Add(Permission permission)
    {
        _permissions.Add(permission);
    }

    public void Remove(Permission permission)
    {
        _permissions.Remove(permission);
    }
}
