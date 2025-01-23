using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleRepository(IdentityContext context) : IRoleRepository
{
    private readonly DbSet<Role> _roles = context.Set<Role>();

    public async Task<ICollection<Role>> GetRolesAsync()
    {
        return await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .ToListAsync();
    }

    public async Task<ICollection<Role>> GetRolesAsync(IEnumerable<RoleCode> roleCodes)
    {
        return await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .Where(x => roleCodes.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<Role> GetRoleAsync(RoleCode roleCode)
    {
        return await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .SingleAsync(x => x.Id == roleCode);
    }

    public void Add(Role role)
    {
        _roles.Add(role);
    }

    public void Remove(Role role)
    {
        _roles.Remove(role);
    }
}
