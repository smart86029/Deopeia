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

internal sealed class TokenService(
    IOptions<JwtOptions> jwtOptions,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IPermissionRepository permissionRepository,
    IRefreshTokenRepository refreshTokenRepository
) : ITokenService
{
    private static readonly TimeSpan LifetimeAccessToken = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan LifetimeIdToken = TimeSpan.FromDays(1);
    private static readonly RSA Rsa;
    private static readonly SigningCredentials Credential;
    private static readonly JsonWebKeyDto Jwk;

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPermissionRepository _permissionRepository = permissionRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;
    private readonly string _issuer = jwtOptions.Value.Issuer;

    static TokenService()
    {
        Rsa = RSA.Create(2048);
        var rsaSecurityKey = new RsaSecurityKey(Rsa) { KeyId = Guid.NewGuid().ToString("N") };
        Credential = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var jsonWebKey = JsonWebKeyConverter.ConvertFromRSASecurityKey(rsaSecurityKey);
        Jwk = new JsonWebKeyDto(
            jsonWebKey.Kty,
            "sig",
            SecurityAlgorithms.RsaSha256,
            jsonWebKey.Kid,
            jsonWebKey.N,
            jsonWebKey.E
        );
    }

    public JsonWebKeyDto JsonWebKey => Jwk;

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

    public async Task<string> GenerateIdTokenAsync(AuthorizationCode authorizationCode)
    {
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var expirationTime = issuedAt + (long)LifetimeIdToken.TotalSeconds;

        var userId = new UserId(authorizationCode.SubjectId!.Value);
        var user = await _userRepository.GetUserAsync(userId);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Iss, _issuer),
            new(JwtRegisteredClaimNames.Sub, authorizationCode.SubjectId.ToString()!),
            new(JwtRegisteredClaimNames.Aud, authorizationCode.ClientId.ToString()),
            new(JwtRegisteredClaimNames.Exp, expirationTime.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Nonce, authorizationCode.Nonce),
            new(JwtRegisteredClaimNames.Amr, "pwd"),
            new(JwtRegisteredClaimNames.Name, user.UserName),
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
            new(JwtRegisteredClaimNames.Aud, grant.ClientId.ToString(), ClaimValueTypes.String),
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
            signingCredentials: Credential
        );
        return handler.WriteToken(token);
    }
}
