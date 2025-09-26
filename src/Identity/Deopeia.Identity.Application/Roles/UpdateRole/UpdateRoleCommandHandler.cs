using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.UpdateRole;

internal class UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    : ICommandHandler<UpdateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async ValueTask<Unit> Handle(
        UpdateRoleCommand command,
        CancellationToken cancellationToken
    )
    {
        var role = await _roleRepository.GetRoleAsync(new RoleCode(command.Code));
        if (command.IsEnabled)
        {
            role.Enable();
        }
        else
        {
            role.Disable();
        }

        var culturesToRemove = role
            .Localizations.Where(x => !command.Localizations.Any(y => y.Culture.Equals(x.Culture)))
            .Select(x => x.Culture)
            .ToArray();
        role.RemoveLocalizations(culturesToRemove);

        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            role.UpdateName(localization.Name, culture);
            role.UpdateDescription(localization.Description, culture);
        }

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
