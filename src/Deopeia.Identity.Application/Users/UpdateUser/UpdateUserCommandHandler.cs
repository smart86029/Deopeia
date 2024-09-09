using Deopeia.Identity.Domain.Roles;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.UpdateUser;

public class UpdateUserCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IRoleRepository roleRepository
) : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.Id));
        if (!request.Password.IsNullOrWhiteSpace())
        {
            user.UpdatePassword(request.Password);
        }

        if (request.IsEnabled)
        {
            user.Enable();
        }
        else
        {
            user.Disable();
        }

        var roleIds = user.UserRoles.Select(x => x.RoleId).ToList();
        var commandRoleIds = request.RoleIds.Select(x => new RoleId(x)).ToList();
        var roleIdsToAssign = commandRoleIds.Except(roleIds);
        var rolesToAssign = await _roleRepository.GetRolesAsync(roleIdsToAssign);
        foreach (var role in rolesToAssign)
        {
            user.AssignRole(role);
        }

        var roleIdsToUnassign = roleIds.Except(commandRoleIds);
        foreach (var roleId in roleIdsToUnassign)
        {
            user.UnassignRole(roleId);
        }

        await _unitOfWork.CommitAsync();
    }
}
