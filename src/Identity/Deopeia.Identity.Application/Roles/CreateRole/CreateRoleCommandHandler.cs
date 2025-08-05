using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.CreateRole;

public class CreateRoleCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRoleRepository roleRepository
) : ICommandHandler<CreateRoleCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async ValueTask<Unit> Handle(
        CreateRoleCommand command,
        CancellationToken cancellationToken
    )
    {
        var en = command.Locales.First(x => x.Culture == "en");
        var role = new Role(command.Code, en.Name, en.Description, command.IsEnabled);
        foreach (var locale in command.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            role.UpdateName(locale.Name, culture);
            role.UpdateDescription(locale.Description, culture);
        }

        _roleRepository.Add(role);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
