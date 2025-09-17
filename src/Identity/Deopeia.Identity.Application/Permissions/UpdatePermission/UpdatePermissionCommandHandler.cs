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

        var localizationsToRemove = permission
            .Localizations.Where(x => !command.Localizations.Any(y => y.Culture.Equals(x.Culture)))
            .ToArray();
        permission.RemoveLocalizations(localizationsToRemove);

        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            permission.UpdateName(localization.Name, culture);
            permission.UpdateDescription(localization.Description, culture);
        }

        await _unitOfWork.CommitAsync();
        return Unit.Value;
    }
}
