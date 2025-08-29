using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.ChangePassword;

internal class ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    public async ValueTask<Unit> Handle(
        ChangePasswordCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(command.Id));
        if (user.Hash(command.CurrentPassword) != user.PasswordHash)
        {
            throw new Exception("Auth.IncorrectPassword");
        }

        user.UpdatePassword(command.NewPassword);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
