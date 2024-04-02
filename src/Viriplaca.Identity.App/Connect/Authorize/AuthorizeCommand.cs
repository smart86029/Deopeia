namespace Viriplaca.Identity.App.Connect.Authorize;

public record AuthorizeCommand(
    string ResponseType,
    string ClientId,
    Uri? RedirectUri,
    ICollection<string> Scopes,
    string State)
    : IRequest<AuthorizeResult>
{
}
