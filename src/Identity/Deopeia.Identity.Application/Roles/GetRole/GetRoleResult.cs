namespace Deopeia.Identity.Application.Roles.GetRole;

public class GetRoleResult
{
    public string Code { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<RoleLocalizationDto> Localizations { get; set; } = [];

    public ICollection<string> PermissionCodes { get; set; } = [];
}
