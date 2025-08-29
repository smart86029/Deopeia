using Deopeia.Identity.Domain.Permissions;

namespace Deopeia.Identity.Application.Permissions.DeletePermission;

internal sealed class DeletePermissionCommandHandler(
    IUnitOfWork unitOfWork,
    IPermissionRepository permissionRepository
) : ICommandHandler<DeletePermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async ValueTask<Unit> Handle(
        DeletePermissionCommand command,
        CancellationToken cancellationToken
    )
    {
        var permission = await _permissionRepository.GetPermissionAsync(
            new PermissionCode(command.Code)
        );
        _permissionRepository.Remove(permission);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
