using Deopeia.Identity.Application.Tokens;
using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Connect.AuthorizationCodeGrant;

internal sealed class AuthorizationCodeGrantCommandHandler(
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository
) : ICommandHandler<AuthorizationCodeGrantCommand, GrantResult>
{
    private readonly TimeSpan _lifetime = TimeSpan.FromMinutes(5);
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public async ValueTask<GrantResult> Handle(
        AuthorizationCodeGrantCommand command,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(
            new ClientId(command.ClientId.ToGuid())
        );
        if (client is null || !client.GrantTypes.HasFlag(GrantTypes.AuthorizationCode))
        {
            return new AuthorizationCodeGrantResult(GrantError.UnauthorizedClient);
        }

        if (command.Code.IsNullOrWhiteSpace())
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        var authorizationCode = await _authorizationCodeRepository.GetAuthorizationCodeAsync(
            command.Code
        );
        if (authorizationCode is null)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        if (authorizationCode.ClientId != client.Id)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        if (authorizationCode.IsExpired)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        if (authorizationCode.Scopes.Count == 0)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidRequest);
        }

        if (command.RedirectUri is null)
        {
            return new AuthorizationCodeGrantResult(GrantError.UnauthorizedClient);
        }

        if (command.RedirectUri != authorizationCode.RedirectUri)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        await ConsumeAsync(authorizationCode);

        var isVerified = _tokenService.VerifyPkce(
            authorizationCode.CodeChallengeMethod,
            authorizationCode.CodeChallenge,
            command.CodeVerifier
        );
        if (!isVerified)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        var subjectId = authorizationCode.SubjectId.ToString();
        if (subjectId.IsNullOrWhiteSpace())
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        var accessToken = await _tokenService.GenerateAccessTokenAsync(authorizationCode);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(authorizationCode, client);
        var idToken = _tokenService.GenerateIdToken(authorizationCode);
        var result = new AuthorizationCodeGrantResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            IdToken = idToken,
            Lifetime = _lifetime,
        };

        return result;
    }

    private async Task ConsumeAsync(AuthorizationCode authorizationCode)
    {
        authorizationCode.Consume();
        await _unitOfWork.CommitAsync();
    }
}
