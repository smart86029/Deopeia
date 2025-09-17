using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.CreateRole;

internal class CreateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    : ICommandHandler<CreateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async ValueTask<Unit> Handle(
        CreateRoleCommand command,
        CancellationToken cancellationToken
    )
    {
        var en = command.Localizations.First(x => x.Culture == "en");
        var role = new Role(command.Code, en.Name, en.Description, command.IsEnabled);
        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            role.UpdateName(localization.Name, culture);
            role.UpdateDescription(localization.Description, culture);
        }

        _roleRepository.Add(role);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
