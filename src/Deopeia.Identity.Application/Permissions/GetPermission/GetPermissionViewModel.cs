namespace Deopeia.Identity.Application.Permissions.GetPermission;

public class GetPermissionViewModel
{
    public string Code { get; set; } = string.Empty;

    public bool IsEnabled { get; set; }

    public ICollection<PermissionLocaleDto> Locales { get; set; } = [];
}
