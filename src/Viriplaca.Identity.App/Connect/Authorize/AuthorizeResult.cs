using Viriplaca.Identity.Domain.Grants.AuthorizationCodes;

namespace Viriplaca.Identity.App.Connect.Authorize;

public class AuthorizeResult(AuthorizationCode authorizationCode, string state)
{
    public string Code { get; private init; } = authorizationCode.Key;

    public Uri RedirectUri { get; private init; } = authorizationCode.RedirectUri;

    public string State { get; private init; } = state;

    public string ReturnUrl => $"{RedirectUri}?code={Code}&state={State}";
}
