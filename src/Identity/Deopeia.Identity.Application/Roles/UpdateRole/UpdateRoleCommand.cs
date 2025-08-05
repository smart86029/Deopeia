namespace Deopeia.Identity.Application.Roles.UpdateRole;

public record UpdateRoleCommand(string Code, bool IsEnabled, ICollection<RoleLocaleDto> Locales)
    : ICommand;
