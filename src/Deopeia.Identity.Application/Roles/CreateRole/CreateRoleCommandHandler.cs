using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.CreateRole;

public class CreateRoleCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRoleRepository roleRepository
) : IRequestHandler<CreateRoleCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var en = request.Locales.First(x => x.Culture == "en");
        var role = new Role(en.Name, en.Description, request.IsEnabled);
        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            role.UpdateName(locale.Name, culture);
            role.UpdateDescription(locale.Description, culture);
        }

        _roleRepository.Add(role);
        await _unitOfWork.CommitAsync();
    }
}
