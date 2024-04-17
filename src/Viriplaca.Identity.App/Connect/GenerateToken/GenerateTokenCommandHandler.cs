using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Viriplaca.Identity.Domain.Clients;
using Viriplaca.Identity.Domain.Grants;
using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

namespace Viriplaca.Identity.App.Connect.GenerateToken;

internal class GenerateTokenCommandHandler(
    IOptions<JwtOptions> jwtOptions,
    IClientRepository clientRepository,
    IAuthorizationCodeRepository authorizationCodeRepository)
    : IRequestHandler<GenerateTokenCommand, TokenResult>
{
    private readonly TimeSpan _expired = TimeSpan.FromMinutes(5);
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository = authorizationCodeRepository;

    public async Task<TokenResult> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientAsync(request.ClientId);
        if (!client.GrantTypes.HasFlag(GrantTypes.AuthorizationCode))
        {
            return TokenResult.FromError(Errors.UnauthorizedClient);
        }

        if (request.Code.IsNullOrWhiteSpace())
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        var authorizationCode = await _authorizationCodeRepository.GetAuthorizationCodeAsync(request.Code);
        if (authorizationCode is null)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        if (authorizationCode.ClientId != client.Id)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        _authorizationCodeRepository.Remove(authorizationCode);

        if (DateTimeOffset.UtcNow > authorizationCode.ExpiresAt)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        if (request.RedirectUri is null)
        {
            return TokenResult.FromError(Errors.UnauthorizedClient);
        }

        if (request.RedirectUri != authorizationCode.RedirectUri)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        if (authorizationCode.Scopes.Count == 0)
        {
            return TokenResult.FromError(Errors.InvalidRequest);
        }

        var isFromClient = IsFromClient(request.CodeVerifier, authorizationCode.CodeChallenge, authorizationCode.CodeChallengeMethod);
        if (!isFromClient)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        var subjectId = authorizationCode.SubjectId.ToString();
        if (subjectId is null)
        {
            return TokenResult.FromError(Errors.InvalidGrant);
        }

        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectId, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.GivenName, string.Empty, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            new(JwtRegisteredClaimNames.Nonce, authorizationCode.Nonce, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Amr, "pwd", ClaimValueTypes.String),
        };
        var idToken = GenerateJwtToken(claims);

        // save
        //var idptoken = new OAuthTokenEntity
        //    {
        //        ClientId = checkClientResult.Client.ClientId,
        //        CreationDate = DateTime.Now,
        //        ReferenceId = Guid.NewGuid().ToString(),
        //        Status = Constants.Statuses.Valid,
        //        Token = idToken,
        //        TokenType = Constants.TokenTypes.JWTIdentityToken,
        //        ExpirationDate = token.ValidTo,
        //        SubjectId = userId,
        //        Revoked = false,
        //    };
        //_context.OAuthTokens.Add(idptoken);
        //_context.SaveChanges();

        // access token
        //var scopesinJWtAccessToken = from m in clientCodeChecker.RequestedScopes.ToList()
        //                             where !OAuth2ServerHelpers.OpenIdConnectScopes.Contains(m)
        //                             select m;
        //var accessTokenResult = generateJWTTokne(scopesinJWtAccessToken, Constants.TokenTypes.JWTAcceseccToken, checkClientResult.Client, userId);
        //SaveJWTTokenInBackStore(checkClientResult.Client.ClientId, accessTokenResult.AccessToken, accessTokenResult.ExpirationDate);

        //result.access_token = accessTokenResult.AccessToken;

        await Task.CompletedTask;
        var result = new TokenResult
        {
            IdToken = idToken,
            State = string.Empty,
            Expired = _expired,
        };

        return result;
    }

    private bool IsFromClient(string codeVerifier, string codeChallenge, string codeChallengeMethod)
    {
        var result = codeChallengeMethod switch
        {
            ChallengeMethods.Plain => codeChallenge == codeVerifier,
            ChallengeMethods.Sha256 => codeChallenge == Base64UrlEncoder.Encode(Encoding.ASCII.GetBytes(codeVerifier).Sha256()),
            _ => false,
        };

        return result;
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();
        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            claims: claims,
            expires: DateTime.Now.Add(_expired),
            signingCredentials: signingCredentials);
        var result = handler.WriteToken(securityToken);

        return result;
    }
}
