using Deopeia.Identity.Domain.Roles;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.UpdateUser;

internal class UpdateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRoleRepository roleRepository
) : ICommandHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async ValueTask<Unit> Handle(
        UpdateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(command.Id));
        if (!command.Password.IsNullOrWhiteSpace())
        {
            user.UpdatePassword(command.Password);
        }

        if (command.IsEnabled)
        {
            user.Enable();
        }
        else
        {
            user.Disable();
        }

        var roleCodes = user.UserRoles.Select(x => x.RoleCode).ToList();
        var commandRoleCodes = command.RoleCodes.Select(x => new RoleCode(x)).ToList();
        var roleCodesToAssign = commandRoleCodes.Except(roleCodes);
        var rolesToAssign = await _roleRepository.GetRolesAsync(roleCodesToAssign);
        foreach (var role in rolesToAssign)
        {
            user.AssignRole(role);
        }

        var roleCodesToUnassign = roleCodes.Except(commandRoleCodes);
        foreach (var roleCode in roleCodesToUnassign)
        {
            user.UnassignRole(roleCode);
        }

        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
