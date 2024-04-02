using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Viriplaca.Identity.App.Connect.GenerateToken;

internal class GenerateTokenCommandHandler(IOptions<JwtOptions> jwtOptions)
    : IRequestHandler<GenerateTokenCommand, TokenResult>
{
    private readonly TimeSpan _expired = TimeSpan.FromMinutes(5);
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<TokenResult> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
    {
        if (!request.Subject.Identity!.IsAuthenticated)
        {
            throw new Exception("InvalidGrant");
        }

        var userId = request.Subject.GetUserId();
        if (userId == Guid.Empty)
        {
            throw new Exception("InvalidGrant");
        }

        var issuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString(), ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.GivenName, string.Empty, ClaimValueTypes.String),
            new(JwtRegisteredClaimNames.Iat, issuedAt.ToString(), ClaimValueTypes.Integer),
            //new (JwtRegisteredClaimNames.Nonce, clientCodeChecker.Nonce, ClaimValueTypes.String)
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


        //access_token = SlAV32hkKG
        //& token_type = bearer
        //& id_token = eyJ0... NiJ9.eyJ1c... I6IjIifX0.DeWt4Qu... ZXso
        //& expires_in = 3600
        //& state = af0ifjsldkj



        await Task.CompletedTask;
        var result = new TokenResult
        {
            IdToken = idToken,
            State = string.Empty,
            Expired = _expired,
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
