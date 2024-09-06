using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Infrastructure.Roles;

internal class RoleRepository(IdentityContext context) : IRoleRepository
{
    private readonly DbSet<Role> _roles = context.Set<Role>();

    public async Task<ICollection<Role>> GetRolesAsync()
    {
        var results = await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .ToListAsync();

        return results;
    }

    public async Task<ICollection<Role>> GetRolesAsync(IEnumerable<RoleId> roleIds)
    {
        var results = await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .Where(x => roleIds.Contains(x.Id))
            .ToListAsync();

        return results;
    }

    public async Task<Role> GetRoleAsync(RoleId roleId)
    {
        var result = await _roles
            .Include(x => x.Locales)
            .Include(x => x.UserRoles)
            .Include(x => x.RolePermissions)
            .SingleAsync(x => x.Id == roleId);

        return result;
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
