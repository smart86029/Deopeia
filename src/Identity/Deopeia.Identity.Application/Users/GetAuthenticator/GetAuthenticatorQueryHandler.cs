using Deopeia.Identity.Domain.Users;

namespace Deopeia.Identity.Application.Users.GetAuthenticator;

internal class GetAuthenticatorQueryHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IAuthenticatorService authenticatorService
) : IQueryHandler<GetAuthenticatorQuery, GetAuthenticatorResult>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthenticatorService _authenticatorService = authenticatorService;

    public async ValueTask<GetAuthenticatorResult> Handle(
        GetAuthenticatorQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await _userRepository.GetUserAsync(new UserId(request.UserId));
        var authenticator = user.Authenticator;
        if (authenticator.SecretKey.IsNullOrWhiteSpace())
        {
            authenticator.CreateSecretKey();
            await _unitOfWork.CommitAsync();
        }

        var setupCode = _authenticatorService.GenerateSetupCode(
            authenticator.SecretKey!,
            user.UserName
        );

        return new GetAuthenticatorResult
        {
            IsEnabled = authenticator.IsEnabled,
            ImageUrl = setupCode.ImageUrl,
            ManualEntryKey = setupCode.ManualEntryKey,
        };
    }
}
