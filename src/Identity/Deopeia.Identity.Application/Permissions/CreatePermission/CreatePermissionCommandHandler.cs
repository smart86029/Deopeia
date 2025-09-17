using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.CreatePermission;

internal sealed class CreatePermissionCommandHandler(
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
        var en = command.Localizations.First(x => x.Culture == "en");
        var permission = new Permission(command.Code, en.Name, en.Description, command.IsEnabled);
        foreach (var localization in command.Localizations)
        {
            var culture = CultureInfo.GetCultureInfo(localization.Culture);
            permission.UpdateName(localization.Name, culture);
            permission.UpdateDescription(localization.Description, culture);
        }

        _permissionRepository.Add(permission);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
