using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.EnableAuthenticator;

public class EnableAuthenticatorCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IAuthenticatorService authenticatorService
) : ICommandHandler<EnableAuthenticatorCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;

    public async ValueTask<Unit> Handle(
        EnableAuthenticatorCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(command.Id));
        var authenticator = user.Authenticator;
        if (authenticator.IsEnabled)
        {
            return Unit.Value;
        }

        var isValid = _authenticatorService.ValidateTwoFactorCode(
            authenticator.SecretKey!,
            command.VerificationCode
        );
        if (!isValid)
        {
            authenticator.IncrementErrorCount();
            throw new Exception("Auth.IncorrectVerificationCode");
        }

        authenticator.Enable();
        await _unitOfWork.CommitAsync();

        return Unit.Value;
    }
}
