using Deopeia.Identity.Domain.Clients;
using Deopeia.Identity.Domain.Grants;
using Deopeia.Identity.Domain.Grants.AuthorizationCodes;

namespace Deopeia.Identity.Application.Tokens;

public interface ITokenService
{
    JsonWebKeyDto JsonWebKey { get; }

    bool VerifyPkce(string codeChallengeMethod, string codeChallenge, string codeVerifier);

    Task<string> GenerateIdTokenAsync(AuthorizationCode authorizationCode);

    Task<string> GenerateAccessTokenAsync(Grant grant);

    Task<string> GenerateRefreshTokenAsync(Grant grant, Client client);
}
