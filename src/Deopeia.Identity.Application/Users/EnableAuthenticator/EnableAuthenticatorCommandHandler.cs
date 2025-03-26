using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.EnableAuthenticator;

public class EnableAuthenticatorCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IAuthenticatorService authenticatorService
) : IRequestHandler<EnableAuthenticatorCommand>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;

    public async Task Handle(
        EnableAuthenticatorCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.Id));
        var authenticator = user.Authenticator;
        if (authenticator.IsEnabled)
        {
            return;
        }

        var isValid = _authenticatorService.ValidateVerificationCode(
            authenticator.SecretKey!,
            request.VerificationCode
        );
        if (!isValid)
        {
            authenticator.IncrementErrorCount();
            throw new LocalizableMessageException("Auth.IncorrectVerificationCode");
        }

        authenticator.Enable();
        await _unitOfWork.CommitAsync();
    }
}
