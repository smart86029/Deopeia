using Deopeia.Identity.Domain.Roles;

namespace Deopeia.Identity.Application.Roles.DeleteRole;

public class DeleteRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
    : ICommandHandler<DeleteRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async ValueTask<Unit> Handle(
        DeleteRoleCommand command,
        CancellationToken cancellationToken
    )
    {
        var role = await _roleRepository.GetRoleAsync(new RoleCode(command.Code));
        _roleRepository.Remove(role);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
