namespace Deopeia.Identity.Application.Roles.CreateRole;

public record CreateRoleCommand(bool IsEnabled, ICollection<RoleLocaleDto> Locales) : IRequest { }
