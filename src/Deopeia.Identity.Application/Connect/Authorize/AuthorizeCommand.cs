namespace Deopeia.Identity.Application.Connect.Authorize;

public record AuthorizeCommand(
    string ResponseType,
    string ClientId,
    Uri? RedirectUri,
    ICollection<string> Scopes,
    string State,
    string CodeChallenge,
    string CodeChallengeMethod
) : IRequest<AuthorizeResult> { }
