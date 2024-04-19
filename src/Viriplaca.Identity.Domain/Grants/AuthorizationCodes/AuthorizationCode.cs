using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

public class AuthorizationCode : Grant
{
    private AuthorizationCode()
    {
    }

    public AuthorizationCode(
        Client client,
        IEnumerable<string> scopes,
        Uri redirectUri,
        string nonce,
        string codeChallenge,
        string codeChallengeMethod)
        : base(GrantTypes.AuthorizationCode, client, scopes, TimeSpan.FromMinutes(1))
    {
        RedirectUri = redirectUri;
        Nonce = nonce;
        CodeChallenge = codeChallenge;
        CodeChallengeMethod = codeChallengeMethod;
    }

    public Uri RedirectUri { get; private init; }

    public string Nonce { get; private init; } = string.Empty;

    public string CodeChallenge { get; private init; } = string.Empty;

    public string CodeChallengeMethod { get; private init; } = string.Empty;
}
