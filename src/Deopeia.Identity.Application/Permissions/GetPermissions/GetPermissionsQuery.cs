namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public record GetPermissionsQuery(string? Code, bool? IsEnabled) : PageQuery<PermissionDto> { }
