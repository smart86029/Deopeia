using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;
using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.App.Authentication.SignIn;

internal class SignInCommandHandler(
    IIdentityUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IAuthorizationCodeRepository authorizationCodeRepository)
    : IRequestHandler<SignInCommand, AuthToken>
{
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository = authorizationCodeRepository;

    public async Task<AuthToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.UserName, request.Password);
        if (user is not null)
        {
            var authorizationCode = await _authorizationCodeRepository.GetAuthorizationCodeAsync(request.Code);
            if (authorizationCode is not null)
            {
                authorizationCode.UpdateSubjectId(user.Id);
                _authorizationCodeRepository.Update(authorizationCode);
                await _unitOfWork.CommitAsync();
            }
        }

        var result = new AuthToken
        {
            SubjectId = user?.Id.ToString() ?? string.Empty,
        };

        return result;
    }
}
