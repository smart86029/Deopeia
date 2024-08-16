using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Domain.Roles;

public class Role : AggregateRoot<RoleId>
{
    private readonly List<UserRole> _userRoles = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Role() { }

    public Role(string name, bool isEnabled)
    {
        name.MustNotBeNullOrWhiteSpace();

        Name = name;
        IsEnabled = isEnabled;
    }

    public string Name { get; private set; } = string.Empty;

    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public void UpdateName(string name)
    {
        name.MustNotBeNullOrWhiteSpace();
        Name = name;
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void AssignPermission(Permission permission)
    {
        if (_rolePermissions.Any(x => x.PermissionId == permission.Id))
        {
            return;
        }

        _rolePermissions.Add(new RolePermission(Id, permission.Id));
    }

    public void UnassignPermission(Permission permission)
    {
        var rolePermission = _rolePermissions.FirstOrDefault(x => x.PermissionId == permission.Id);
        if (rolePermission is null)
        {
            return;
        }

        _rolePermissions.Remove(rolePermission);
    }
}
