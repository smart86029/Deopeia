using Deopeia.Identity.Application.Connect.Authorize;

namespace Deopeia.Identity.Api.Models.Connect;

public class AuthorizeRequest
{
    [ModelBinder(Name = "response_type")]
    public string ResponseType { get; init; } = string.Empty;

    [ModelBinder(Name = "client_id")]
    public string ClientId { get; init; } = string.Empty;

    [ModelBinder(Name = "redirect_uri")]
    public Uri? RedirectUri { get; init; }

    public string Scope { get; init; } = string.Empty;

    public string State { get; init; } = string.Empty;

    [ModelBinder(Name = "code_challenge")]
    public string CodeChallenge { get; init; } = string.Empty;

    [ModelBinder(Name = "code_challenge_method")]
    public string CodeChallengeMethod { get; init; } = string.Empty;

    public ICollection<string> Scopes =>
        Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];

    public AuthorizeCommand ToCommand(Guid? subjectId)
    {
        var result = new AuthorizeCommand(
            ResponseType,
            subjectId,
            ClientId,
            RedirectUri,
            Scopes,
            State,
            CodeChallenge,
            CodeChallengeMethod
        );

        return result;
    }
}
