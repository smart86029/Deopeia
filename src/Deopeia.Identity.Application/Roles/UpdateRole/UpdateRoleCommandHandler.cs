using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.UpdateRole;

public class UpdateRoleCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IRoleRepository roleRepository
) : IRequestHandler<UpdateRoleCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetRoleAsync(new RoleId(request.Id));
        if (request.IsEnabled)
        {
            role.Enable();
        }
        else
        {
            role.Disable();
        }

        var removed = role
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        role.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            role.UpdateName(locale.Name, CultureInfo.GetCultureInfo(locale.Culture));
            role.UpdateDescription(locale.Description, CultureInfo.GetCultureInfo(locale.Culture));
        }

        await _unitOfWork.CommitAsync();
    }
}
