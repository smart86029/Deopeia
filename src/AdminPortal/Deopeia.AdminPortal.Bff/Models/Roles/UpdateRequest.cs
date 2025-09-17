namespace Deopeia.AdminPortal.Bff.Models.Roles;

public sealed record UpdateRequest(
    string Code,
    bool IsEnabled,
    IReadOnlyList<RoleLocalization> Localizations,
    IReadOnlyList<string> PermissionCodes
);
