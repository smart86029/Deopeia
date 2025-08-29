namespace Deopeia.AdminPortal.Bff.Models.Roles;

public sealed record RoleResponse(
    string Code,
    bool IsEnabled,
    IReadOnlyList<RoleLocale> Locales,
    IReadOnlyList<string> PermissionCodes
);
