using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Permissions;

public class Permission : AggregateRoot, ILocalizable<PermissionLocale>
{
    private readonly EntityLocaleCollection<PermissionLocale> _locales = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Permission() { }

    public Permission(string code, bool isEnabled)
    {
        code.MustNotBeNullOrWhiteSpace();

        Code = code.Trim();
        IsEnabled = isEnabled;
    }

    public string Code { get; private set; } = string.Empty;

    public string Name => _locales[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _locales[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public IReadOnlyCollection<PermissionLocale> Locales => _locales;

    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;

    public void UpdateCode(string code)
    {
        code.MustNotBeNullOrWhiteSpace();
        Code = code.Trim();
    }

    public void UpdateName(string name, CultureInfo culture)
    {
        _locales[culture].UpdateName(name);
    }

    public void UpdateDescription(string description, CultureInfo culture)
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
}
