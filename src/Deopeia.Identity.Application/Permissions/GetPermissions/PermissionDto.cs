namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public class PermissionDto
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; }
}
