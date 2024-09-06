using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.CreateUser;

public class CreateUserCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository
) : IRequestHandler<CreateUserCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.UserName, request.Password, request.IsEnabled);

        _userRepository.Add(user);
        await _unitOfWork.CommitAsync();
    }
}
