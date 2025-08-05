using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.UpdateRole;

internal class UpdateRoleCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRoleRepository roleRepository
) : ICommandHandler<UpdateRoleCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
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

        var removed = role
            .Locales.Where(x => !command.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        role.RemoveLocales(removed);

        foreach (var locale in command.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            role.UpdateName(locale.Name, culture);
            role.UpdateDescription(locale.Description, culture);
        }

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
