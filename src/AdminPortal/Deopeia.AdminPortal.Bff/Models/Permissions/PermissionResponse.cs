namespace Deopeia.AdminPortal.Bff.Models.Permissions;

public sealed record PermissionResponse(
    string Code,
    bool IsEnabled,
    IReadOnlyList<PermissionLocale> Locales
);
