namespace Deopeia.Identity.Application.Roles.CreateRole;

public record CreateRoleCommand(
    string Code,
    bool IsEnabled,
    ICollection<RoleLocalizationDto> Localizations
) : ICommand;
