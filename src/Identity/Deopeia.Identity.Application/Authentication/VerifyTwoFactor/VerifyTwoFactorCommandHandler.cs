using Deopeia.Identity.Application.Users;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Authentication.VerifyTwoFactor;

internal sealed class VerifyTwoFactorCommandHandler(
    IUserRepository userRepository,
    IAuthenticatorService authenticatorService
) : ICommandHandler<VerifyTwoFactorCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;

    public async ValueTask<bool> Handle(
        VerifyTwoFactorCommand command,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(command.UserId));
        if (user is null || !user.Authenticator.IsEnabled)
        {
            return false;
        }

        return _authenticatorService.ValidateTwoFactorCode(
            user.Authenticator.SecretKey,
            command.TwoFactorCode
        );
    }
}
