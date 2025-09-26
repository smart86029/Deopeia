using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Domain.Permissions;

public class Permission
    : AggregateRoot<PermissionCode>,
        ILocalizable<PermissionLocalization, PermissionCode>
{
    private readonly EntityLocalizationCollection<
        PermissionLocalization,
        PermissionCode
    > _localizations = [];
    private readonly List<RolePermission> _rolePermissions = [];

    private Permission() { }

    public Permission(string code, string name, string? description, bool isEnabled)
        : base(new PermissionCode(code))
    {
        _localizations.Default.UpdateName(name);
        _localizations.Default.UpdateDescription(description);
        IsEnabled = isEnabled;
    }

    public string Name => _localizations[CultureInfo.CurrentCulture]?.Name ?? string.Empty;

    public string? Description => _localizations[CultureInfo.CurrentCulture]?.Description;

    public bool IsEnabled { get; private set; }

    public IReadOnlyList<PermissionLocalization> Localizations => _localizations;

    public IReadOnlyList<RolePermission> RolePermissions => _rolePermissions;

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

    public void RemoveLocalizations(IEnumerable<CultureInfo> cultures)
    {
        _localizations.RemoveRange(cultures);
    }
}
