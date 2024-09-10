namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public record GetPermissionsQuery(bool? IsEnabled) : PageQuery<PermissionDto> { }
