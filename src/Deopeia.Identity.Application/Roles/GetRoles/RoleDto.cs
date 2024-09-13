namespace Deopeia.Identity.Application.Roles.GetRoles;

public class RoleDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsEnabled { get; set; }
}
