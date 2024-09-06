using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.UpdateUser;

public class UpdateUserCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<UpdateUserCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

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

        await _unitOfWork.CommitAsync();
    }
}
