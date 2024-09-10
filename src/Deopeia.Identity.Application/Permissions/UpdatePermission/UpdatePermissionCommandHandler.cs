using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.UpdatePermission;

public class UpdatePermissionCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IPermissionRepository permissionRepository
) : IRequestHandler<UpdatePermissionCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetPermissionAsync(
            new PermissionId(request.Id)
        );

        permission.UpdateCode(request.Code);

        if (request.IsEnabled)
        {
            permission.Enable();
        }
        else
        {
            permission.Disable();
        }

        var removed = permission
            .Locales.Where(x => !request.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        permission.RemoveLocales(removed);

        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            permission.UpdateName(locale.Name, culture);
            permission.UpdateDescription(locale.Description, culture);
        }

        await _unitOfWork.CommitAsync();
    }
}
