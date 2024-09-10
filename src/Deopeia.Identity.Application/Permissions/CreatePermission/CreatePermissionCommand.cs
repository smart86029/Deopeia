namespace Deopeia.Identity.Application.Permissions.CreatePermission;

public record CreatePermissionCommand(
    string Code,
    bool IsEnabled,
    ICollection<PermissionLocaleDto> Locales
) : IRequest { }
