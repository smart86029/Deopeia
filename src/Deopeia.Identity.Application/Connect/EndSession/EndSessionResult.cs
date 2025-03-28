namespace Deopeia.Identity.Application.Connect.EndSession;

public class EndSessionResult(Uri postLogoutRedirectUri, string state)
{
    public Uri RedirectUri { get; private init; } = postLogoutRedirectUri;

    public string State { get; private init; } = state;

    public string ReturnUrl => $"{RedirectUri}?state={State}";
}
