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

        var roleCodes = user.UserRoles.Select(x => x.RoleCode).ToList();
        var commandRoleCodes = request.RoleCodes.Select(x => new RoleCode(x)).ToList();
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
    }
}
