using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.ChangePassword;

public class ChangePasswordCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<ChangePasswordCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.Id));
        if (user.Hash(request.CurrentPassword) != user.PasswordHash)
        {
            throw new LocalizableMessageException("Auth.IncorrectPassword");
        }

        user.UpdatePassword(request.NewPassword);
        await _unitOfWork.CommitAsync();
    }
}
