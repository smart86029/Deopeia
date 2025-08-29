namespace Deopeia.Identity.Application.Roles.GetRoles;

public sealed record GetRolesQuery(bool? IsEnabled) : PagedQuery<RoleDto>;
