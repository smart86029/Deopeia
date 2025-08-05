namespace Deopeia.Identity.Domain.Roles;

public interface IRoleRepository : IRepository<Role, RoleCode>
{
    Task<ICollection<Role>> GetRolesAsync();

    Task<ICollection<Role>> GetRolesAsync(IEnumerable<RoleCode> roleCodes);

    Task<Role> GetRoleAsync(RoleCode roleCode);

    void Add(Role role);

    void Remove(Role role);
}
