namespace Viriplaca.Identity.Api.Models.Connect;

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

    public ICollection<string> Scopes => Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? [];
}
