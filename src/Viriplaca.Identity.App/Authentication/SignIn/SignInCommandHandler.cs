using Viriplaca.Identity.Domain.Users;

namespace Viriplaca.Identity.App.Authentication.SignIn;

internal class SignInCommandHandler(IUserRepository userRepository)
    : IRequestHandler<SignInCommand, AuthToken>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<AuthToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.UserName, request.Password);
        var result = new AuthToken
        {
            SubjectId = user.Id.ToString(),
        };

        return result;
    }
}
