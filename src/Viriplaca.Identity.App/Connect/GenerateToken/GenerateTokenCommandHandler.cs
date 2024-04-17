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
    private readonly TimeSpan _lifetime = TimeSpan.FromMinutes(5);
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IAuthorizationCodeRepository _authorizationCodeRepository = authorizationCodeRepository;

    public async Task<TokenResult> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.GetClientAsync(request.ClientId);
        if (!client.GrantTypes.HasFlag(GrantTypes.AuthorizationCode))
        {
            return new TokenResult(TokenError.UnauthorizedClient);
        }

        if (request.Code.IsNullOrWhiteSpace())
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        var authorizationCode = await _authorizationCodeRepository.GetAuthorizationCodeAsync(request.Code);
        if (authorizationCode is null)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        if (authorizationCode.ClientId != client.Id)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        _authorizationCodeRepository.Remove(authorizationCode);

        if (DateTimeOffset.UtcNow > authorizationCode.ExpiresAt)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        if (request.RedirectUri is null)
        {
            return new TokenResult(TokenError.UnauthorizedClient);
        }

        if (request.RedirectUri != authorizationCode.RedirectUri)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        if (authorizationCode.Scopes.Count == 0)
        {
            return new TokenResult(TokenError.InvalidRequest);
        }

        var isFromClient = IsFromClient(request.CodeVerifier, authorizationCode.CodeChallenge, authorizationCode.CodeChallengeMethod);
        if (!isFromClient)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        var subjectId = authorizationCode.SubjectId.ToString();
        if (subjectId is null)
        {
            return new TokenResult(TokenError.InvalidGrant);
        }

        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Jti, aud, permission
        var accessToken = GenerateJwtToken(new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectId, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            new("scope", string.Join(' ', authorizationCode.Scopes)),
        });
        var idToken = GenerateJwtToken(new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectId, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.GivenName, string.Empty, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            new(JwtRegisteredClaimNames.Nonce, authorizationCode.Nonce, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Amr, "pwd", ClaimValueTypes.String),
        });

        var result = new TokenResult
        {
            AccessToken = accessToken,
            RefreshToken = Guid.NewGuid().ToString("N"),
            IdToken = idToken,
            Lifetime = _lifetime,
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
            expires: DateTime.Now.Add(_lifetime),
            signingCredentials: signingCredentials);
        var result = handler.WriteToken(securityToken);

        return result;
    }
}
