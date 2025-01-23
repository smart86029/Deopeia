namespace Deopeia.Identity.Application.Roles.GetRole;

public class GetRoleViewModel
{
    public string Code { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<RoleLocaleDto> Locales { get; set; } = [];

    public ICollection<string> PermissionCodes { get; set; } = [];
}
