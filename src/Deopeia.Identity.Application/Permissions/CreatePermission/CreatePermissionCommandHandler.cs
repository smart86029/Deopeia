using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.CreatePermission;

public class CreatePermissionCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IPermissionRepository permissionRepository
) : IRequestHandler<CreatePermissionCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var en = request.Locales.First(x => x.Culture == "en");
        var permission = new Permission(request.Code, en.Name, en.Description, request.IsEnabled);
        foreach (var locale in request.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            permission.UpdateName(locale.Name, culture);
            permission.UpdateDescription(locale.Description, culture);
        }

        _permissionRepository.Add(permission);
        await _unitOfWork.CommitAsync();
    }
}
