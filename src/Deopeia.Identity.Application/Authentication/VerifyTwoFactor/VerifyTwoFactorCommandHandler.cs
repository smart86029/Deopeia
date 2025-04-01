using Deopeia.Identity.Application.Users;
using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Authentication.VerifyTwoFactor;

public class VerifyTwoFactorCommandHandler(
    IUserRepository userRepository,
    IAuthenticatorService authenticatorService
) : IRequestHandler<VerifyTwoFactorCommand, bool>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;

    public async Task<bool> Handle(
        VerifyTwoFactorCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.UserId));
        if (user is null || !user.Authenticator.IsEnabled)
        {
            return false;
        }

        return _authenticatorService.ValidateTwoFactorCode(
            user.Authenticator.SecretKey!,
            request.TwoFactorCode
        );
    }
}
