using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;
using Deopeia.Identity.Domain.Grants.RefreshTokens;
using Deopeia.Identity.Domain.Permissions;
using Microsoft.IdentityModel.Tokens;

namespace Deopeia.Identity.Application.Connect.AuthorizationCodeGrant;

internal class AuthorizationCodeGrantCommandHandler(
    IOptions<JwtOptions> jwtOptions,
    IUnitOfWork unitOfWork,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository,
    IPermissionRepository permissionRepository,
    IRefreshTokenRepository refreshTokenRepository
)
    : GrantCommandHandler<AuthorizationCodeGrantCommand>(
        jwtOptions,
        unitOfWork,
        permissionRepository,
        refreshTokenRepository
    )
{
    private readonly TimeSpan _lifetime = TimeSpan.FromMinutes(5);
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository =
        authorizationCodeRepository;

    public override async ValueTask<GrantResult> Handle(
        AuthorizationCodeGrantCommand command,
        CancellationToken cancellationToken
    )
    {
        var client = await _clientRepository.GetClientAsync(command.ClientId);
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

        var isFromClient = IsFromClient(
            command.CodeVerifier,
            authorizationCode.CodeChallenge,
            authorizationCode.CodeChallengeMethod
        );
        if (!isFromClient)
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        var subjectId = authorizationCode.SubjectId.ToString();
        if (subjectId.IsNullOrWhiteSpace())
        {
            return new AuthorizationCodeGrantResult(GrantError.InvalidGrant);
        }

        var accessToken = await GenerateAccessTokenAsync(authorizationCode);
        var refreshToken = await GenerateRefreshTokenAsync(client, authorizationCode);
        var idToken = GenerateIdToken(authorizationCode);
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

    private static bool IsFromClient(
        string codeVerifier,
        string codeChallenge,
        string codeChallengeMethod
    )
    {
        var result = codeChallengeMethod switch
        {
            ChallengeMethods.Plain => codeChallenge == codeVerifier,
            ChallengeMethods.Sha256 => codeChallenge
                == Base64UrlEncoder.Encode(Encoding.ASCII.GetBytes(codeVerifier).Sha256()),
            _ => false,
        };

        return result;
    }
}
