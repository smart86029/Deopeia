namespace Deopeia.Identity.Application.Permissions.CreatePermission;

public sealed record CreatePermissionCommand(
    string Code,
    bool IsEnabled,
    ICollection<PermissionLocaleDto> Locales
) : ICommand;
