using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.CreatePermission;

internal class CreatePermissionCommandHandler(
    IUnitOfWork unitOfWork,
    IPermissionRepository permissionRepository
) : ICommandHandler<CreatePermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async ValueTask<Unit> Handle(
        CreatePermissionCommand command,
        CancellationToken cancellationToken
    )
    {
        var en = command.Locales.First(x => x.Culture == "en");
        var permission = new Permission(command.Code, en.Name, en.Description, command.IsEnabled);
        foreach (var locale in command.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            permission.UpdateName(locale.Name, culture);
            permission.UpdateDescription(locale.Description, culture);
        }

        _permissionRepository.Add(permission);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
