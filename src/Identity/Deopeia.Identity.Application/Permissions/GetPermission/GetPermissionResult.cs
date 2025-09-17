namespace Deopeia.Identity.Application.Permissions.GetPermission;

public sealed class GetPermissionResult
{
    public string Code { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<PermissionLocalizationDto> Localizations { get; set; } = [];
}
