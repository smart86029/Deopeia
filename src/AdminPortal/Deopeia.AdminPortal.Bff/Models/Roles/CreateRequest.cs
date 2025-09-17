namespace Deopeia.AdminPortal.Bff.Models.Roles;

public sealed record CreateRequest(
    string Code,
    bool IsEnabled,
    IReadOnlyList<RoleLocalization> Localizations,
    IReadOnlyList<string> PermissionCodes
);
