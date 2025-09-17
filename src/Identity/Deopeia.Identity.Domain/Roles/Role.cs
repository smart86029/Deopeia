using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Domain.Roles;

public class Role : AggregateRoot<RoleCode>, ILocalizable<RoleLocalization, RoleCode>
{
    private readonly EntityLocalizationCollection<RoleLocalization, RoleCode> _localizations = [];
    private readonly List<UserRole> _userRoles = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Role() { }

    public Role(string code, string name, string? description, bool isEnabled)
        : base(new RoleCode(code))
    {
        _localizations.Default.UpdateName(name);
        _localizations.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
    }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _localizations[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public IReadOnlyList<RoleLocalization> Localizations => _localizations;

    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public void UpdateName(string name, CultureInfo culture)
    {
        _localizations[culture].UpdateName(name);
    }

    public void UpdateDescription(string? description, CultureInfo culture)
    {
        _localizations[culture].UpdateDescription(description);
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    public void RemoveLocalizations(IEnumerable<RoleLocalization> localizations)
    {
        _localizations.RemoveRange(localizations);
    }

    public void AssignPermission(Permission permission)
    {
        if (_rolePermissions.Any(x => x.PermissionCode == permission.Id))
        {
            return;
        }

        _rolePermissions.Add(new RolePermission(Id, permission.Id));
    }

    public void UnassignPermission(Permission permission)
    {
        var rolePermission = _rolePermissions.FirstOrDefault(x =>
            x.PermissionCode == permission.Id
        );
        if (rolePermission is null)
        {
            return;
        }

        _rolePermissions.Remove(rolePermission);
    }
}
