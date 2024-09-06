namespace Deopeia.Identity.Application.Roles.UpdateRole;

public record UpdateRoleCommand(Guid Id, bool IsEnabled, ICollection<RoleLocaleDto> Locales)
    : IRequest { }
