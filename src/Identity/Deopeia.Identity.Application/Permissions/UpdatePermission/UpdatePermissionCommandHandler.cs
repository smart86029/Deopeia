using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.UpdatePermission;

internal class UpdatePermissionCommandHandler(
    IUnitOfWork unitOfWork,
    IPermissionRepository permissionRepository
) : ICommandHandler<UpdatePermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async ValueTask<Unit> Handle(
        UpdatePermissionCommand command,
        CancellationToken cancellationToken
    )
    {
        var permission = await _permissionRepository.GetPermissionAsync(
            new PermissionCode(command.Code)
        );

        if (command.IsEnabled)
        {
            permission.Enable();
        }
        else
        {
            permission.Disable();
        }

        var removed = permission
            .Locales.Where(x => !command.Locales.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        permission.RemoveLocales(removed);

        foreach (var locale in command.Locales)
        {
            var culture = CultureInfo.GetCultureInfo(locale.Culture);
            permission.UpdateName(locale.Name, culture);
            permission.UpdateDescription(locale.Description, culture);
        }

        await _unitOfWork.CommitAsync();
        return Unit.Value;
    }
}
