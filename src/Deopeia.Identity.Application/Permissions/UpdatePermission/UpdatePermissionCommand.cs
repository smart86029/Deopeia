namespace Deopeia.Identity.Application.Permissions.UpdatePermission;

public record UpdatePermissionCommand(
    string Code,
    bool IsEnabled,
    ICollection<PermissionLocaleDto> Locales
) : IRequest { }
