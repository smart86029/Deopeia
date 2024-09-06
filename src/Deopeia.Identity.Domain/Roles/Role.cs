using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Domain.Roles;

public class Role : AggregateRoot<RoleId>, ILocalizable<RoleLocale, RoleId>
{
    private readonly EntityLocaleCollection<RoleLocale, RoleId> _locales = [];
    private readonly List<UserRole> _userRoles = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Role() { }

    public Role(string name, string? description, bool isEnabled)
    {
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<RoleLocale> Locales => _locales;

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _locales[culture].UpdateDescription(description);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void RemoveLocales(IEnumerable<RoleLocale> locales)
    {
        _locales.Remove(locales);
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
