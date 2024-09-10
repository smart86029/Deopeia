namespace Deopeia.Identity.Application.Permissions.UpdatePermission;

public record UpdatePermissionCommand(
    Guid Id,
    string Code,
    bool IsEnabled,
    ICollection<PermissionLocaleDto> Locales
) : IRequest { }
