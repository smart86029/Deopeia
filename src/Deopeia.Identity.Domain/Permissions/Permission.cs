using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Permissions;

public class Permission
    : AggregateRoot<PermissionCode>,
        ILocalizable<PermissionLocale, PermissionCode>
{
    private readonly EntityLocaleCollection<PermissionLocale, PermissionCode> _locales = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Permission() { }

    public Permission(string code, string name, string? description, bool isEnabled)
        : base(new PermissionCode(code))
    {
        _locales.Default.UpdateName(name);
        _locales.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
    }

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<PermissionLocale> Locales => _locales;

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;

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

    public void RemoveLocales(IEnumerable<PermissionLocale> locales)
    {
        _locales.Remove(locales);
    }
}
