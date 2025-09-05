using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Deopeia.Common.Application;
using Deopeia.Identity.Application.Tokens;
using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;
using Deopeia.Identity.Domain.Grants.RefreshTokens;
using Deopeia.Identity.Domain.Permissions;
using Deopeia.Identity.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Deopeia.Identity.Infrastructure.Tokens;

internal sealed class TokenService : ITokenService
{
    private static readonly TimeSpan LifetimeAccessToken = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan LifetimeIdToken = TimeSpan.FromDays(1);

    private readonly IUnitOfWork _unitOfWork;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly string _issuer;
    private readonly SigningCredentials _credential;
    private readonly JsonWebKeyDto _jsonWebKey;

    public TokenService(
        IOptions<JwtOptions> jwtOptions,
        IUnitOfWork unitOfWork,
        IPermissionRepository permissionRepository,
        IRefreshTokenRepository refreshTokenRepository
    )
    {
        _unitOfWork = unitOfWork;
        _permissionRepository = permissionRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _issuer = jwtOptions.Value.Issuer;

        using var rsa = RSA.Create(2048);
        var rsaSecurityKey = new RsaSecurityKey(rsa) { KeyId = Guid.NewGuid().ToString("N") };
        _credential = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var jsonWebKey = JsonWebKeyConverter.ConvertFromRSASecurityKey(rsaSecurityKey);
        _jsonWebKey = new JsonWebKeyDto(
            jsonWebKey.Kty,
            "sig",
            SecurityAlgorithms.RsaSha256,
            jsonWebKey.Kid,
            jsonWebKey.E,
            jsonWebKey.N
        );
    }

    public JsonWebKeyDto JsonWebKey => _jsonWebKey;

    public bool VerifyPkce(string codeChallengeMethod, string codeChallenge, string codeVerifier)
    {
        return codeChallengeMethod switch
        {
            ChallengeMethods.Plain => codeChallenge == codeVerifier,
            ChallengeMethods.Sha256 => codeChallenge
                == Base64UrlEncoder.Encode(Encoding.ASCII.GetBytes(codeVerifier).Sha256()),
            _ => false,
        };
    }

    public string GenerateIdToken(AuthorizationCode authorizationCode)
    {
        var subjectId = authorizationCode.SubjectId.ToString()!;
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectId, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.GivenName, string.Empty, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Nonce, authorizationCode.Nonce, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Amr, "pwd", ClaimValueTypes.String),
        };
        return CreateToken(claims, LifetimeIdToken);
    }

    public async Task<string> GenerateAccessTokenAsync(Grant grant)
    {
        // Jti, aud, permission
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var permissionCodes = await _permissionRepository.GetPermissionCodesAsync(
            new UserId(grant.SubjectId!.Value)
        );
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, grant.SubjectId.ToString()!, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Aud, "https://localhost:7042"),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer64),
            new("scope", string.Join(' ', grant.Scopes)),
            new("permissions", string.Join(',', permissionCodes.Select(x => x.Value))),
        };
        return CreateToken(claims, LifetimeAccessToken);
    }

    public async Task<string> GenerateRefreshTokenAsync(Grant grant, Client client)
    {
        if (grant.SubjectId is null)
        {
            throw new Exception();
        }

        var refreshToken = new RefreshToken(grant.SubjectId.Value, client, grant.Scopes);
        _refreshTokenRepository.Add(refreshToken);
        await _unitOfWork.CommitAsync();

        return refreshToken.Key;
    }

    private string CreateToken(IEnumerable<Claim> claims, TimeSpan lifetime)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = new JwtSecurityToken(
            issuer: _issuer,
            claims: claims,
            expires: DateTime.UtcNow.Add(lifetime),
            signingCredentials: _credential
        );
        return handler.WriteToken(token);
    }
}
