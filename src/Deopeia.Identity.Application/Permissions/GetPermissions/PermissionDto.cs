namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public class PermissionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }
}
