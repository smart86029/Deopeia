using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.CreateUser;

public class CreateUserCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository
) : ICommandHandler<CreateUserCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    public async ValueTask<Unit> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = new User(command.UserName, command.Password, command.IsEnabled);

        _userRepository.Add(user);
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
