namespace Deopeia.AdminPortal.Bff.Models.Permissions;

public sealed record CreateRequest(
    string Code,
    bool IsEnabled,
    IReadOnlyList<PermissionLocale> Locales
);
