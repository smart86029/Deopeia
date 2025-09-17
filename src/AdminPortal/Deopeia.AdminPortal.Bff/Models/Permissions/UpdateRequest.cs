namespace Deopeia.AdminPortal.Bff.Models.Permissions;

public sealed record UpdateRequest(
    string Code,
    bool IsEnabled,
    IReadOnlyList<PermissionLocalization> Localizations
);
