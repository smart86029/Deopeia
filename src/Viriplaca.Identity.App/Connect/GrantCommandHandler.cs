using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Viriplaca.Identity.Domain.Clients;
using Viriplaca.Identity.Domain.Grants;
using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;
using Viriplaca.Identity.Domain.Grants.RefreshTokens;

namespace Viriplaca.Identity.App.Connect;

internal abstract class GrantCommandHandler<TCommand>(
    IOptions<JwtOptions> jwtOptions,
    IIdentityUnitOfWork unitOfWork,
    IRefreshTokenRepository refreshTokenRepository)
    : IRequestHandler<TCommand, GrantResult>
    where TCommand : GrantCommand
{
    private static readonly TimeSpan LifetimeAccessToken = TimeSpan.FromMinutes(5);
    private static readonly TimeSpan LifetimeIdToken = TimeSpan.FromDays(1);

    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly IIdentityUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

    public abstract Task<GrantResult> Handle(TCommand request, CancellationToken cancellationToken);

    protected string GenerateAccessToken(Grant grant)
    {
        // Jti, aud, permission
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, grant.SubjectId.ToString()!, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            new("scope", string.Join(' ', grant.Scopes)),
        };

        return GenerateJwtToken(claims, LifetimeAccessToken);
    }

    protected async Task<string> GenerateRefreshTokenAsync(Client client, Grant grant)
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

    protected string GenerateIdToken(AuthorizationCode authorizationCode)
    {
        var subjectId = authorizationCode.SubjectId.ToString()!;
        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subjectId, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.GivenName, string.Empty, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            new(JwtRegisteredClaimNames.Nonce, authorizationCode.Nonce, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Amr, "pwd", ClaimValueTypes.String),
        };

        return GenerateJwtToken(claims, LifetimeIdToken);
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims, TimeSpan lifetime)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();
        var securityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            claims: claims,
            expires: DateTime.Now.Add(lifetime),
            signingCredentials: signingCredentials);
        var result = handler.WriteToken(securityToken);

        return result;
    }
}
