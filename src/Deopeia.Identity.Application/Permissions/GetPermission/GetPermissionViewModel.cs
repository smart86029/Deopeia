namespace Deopeia.Identity.Application.Permissions.GetPermission;

public class GetPermissionViewModel
{
    public Guid Id { get; set; }

    public bool IsEnabled { get; set; }

    public ICollection<PermissionLocaleDto> Locales { get; set; } = [];
}
