namespace Deopeia.Identity.Application.Roles.GetRoles;

public record GetRolesQuery(bool? IsEnabled) : PagedQuery<RoleDto>;
