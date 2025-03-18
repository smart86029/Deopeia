using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.GetAuthenticator;

internal class GetAuthenticatorQueryHandler(
    IAuthenticatorService authenticatorService,
    IUserRepository userRepository
) : IRequestHandler<GetAuthenticatorQuery, GetAuthenticatorResult>
{
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<GetAuthenticatorResult> Handle(
        GetAuthenticatorQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.UserId));
        var authenticator = user.Authenticator;
        if (authenticator.SecretKey.IsNullOrWhiteSpace())
        {
            authenticator.CreateSecretKey();
        }

        var setupCode = _authenticatorService.GenerateSetupCode(
            authenticator.SecretKey!,
            user.UserName
        );

        return new GetAuthenticatorResult
        {
            IsBound = authenticator.BindingStatus == BindingStatus.Bound,
            ImageUrl = setupCode.ImageUrl,
            ManualEntryKey = setupCode.ManualEntryKey,
        };
    }
}
