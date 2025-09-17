namespace Deopeia.Identity.Application.Permissions;

public class PermissionLocalizationDto
{
    public string Culture { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
