using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Roles;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Infrastructure.Permissions;

internal class PermissionRepository(IdentityContext context) : IPermissionRepository
{
    private readonly IdentityContext _context = context;
    private readonly DbSet<Permission> _permissions = context.Set<Permission>();

    public async Task<ICollection<Permission>> GetPermissionsAsync()
    {
        return await _permissions.Include(x => x.Locales).ToListAsync();
    }

    public async Task<ICollection<Permission>> GetPermissionsAsync(
        IEnumerable<PermissionCode> permissionCodes
    )
    {
        return await _permissions
            .Include(x => x.Locales)
            .Where(x => permissionCodes.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<ICollection<PermissionCode>> GetPermissionCodesAsync(UserId userId)
    {
        return await _context
            .Set<UserRole>()
            .Where(x => x.UserId == userId)
            .Join(
                _context.Set<RolePermission>(),
                x => x.RoleCode,
                y => y.RoleCode,
                (x, y) => y.PermissionCode
            )
            .Distinct()
            .ToListAsync();
    }

    public async Task<Permission> GetPermissionAsync(PermissionCode permissionCode)
    {
        return await _permissions.Include(x => x.Locales).SingleAsync(x => x.Id == permissionCode);
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
