namespace Deopeia.Identity.Application.Roles.GetRole;

public class GetRoleViewModel
{
    public Guid Id { get; set; }

    public bool IsEnabled { get; set; }

    public ICollection<RoleLocaleDto> Locales { get; set; } = [];
}
