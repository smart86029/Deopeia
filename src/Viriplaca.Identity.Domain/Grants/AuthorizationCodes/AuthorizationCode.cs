using Viriplaca.Identity.Domain.Clients;

namespace Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

public class AuthorizationCode : Grant
{
    private readonly List<string> _scopes = [];

    private AuthorizationCode()
    {
    }

    public AuthorizationCode(
        Client client,
        Uri redirectUri,
        IEnumerable<string> scopes,
        string nonce,
        string codeChallenge,
        string codeChallengeMethod)
        : base(GrantTypes.AuthorizationCode, client, TimeSpan.FromMinutes(1))
    {
        RedirectUri = redirectUri;
        _scopes.AddRange(scopes);
        Nonce = nonce;
        CodeChallenge = codeChallenge;
        CodeChallengeMethod = codeChallengeMethod;
    }

    public Uri RedirectUri { get; private init; }

    public IReadOnlyCollection<string> Scopes => _scopes.AsReadOnly();

    public string Nonce { get; private init; } = string.Empty;

    public string CodeChallenge { get; private init; } = string.Empty;

    public string CodeChallengeMethod { get; private init; } = string.Empty;
}
