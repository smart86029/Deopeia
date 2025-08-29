namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public sealed record GetPermissionsQuery(string? Code, bool? IsEnabled) : PagedQuery<PermissionDto>;
