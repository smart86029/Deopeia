namespace Deopeia.Identity.Domain.Roles;

public interface IRoleRepository : IRepository<Role, RoleId>
{
    Task<ICollection<Role>> GetRolesAsync();

    Task<ICollection<Role>> GetRolesAsync(IEnumerable<RoleId> roleIds);

    Task<Role> GetRoleAsync(RoleId roleId);

    void Add(Role role);

    void Remove(Role role);
}
