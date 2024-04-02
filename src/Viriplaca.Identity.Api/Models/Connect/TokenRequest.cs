namespace Viriplaca.Identity.Api.Models.Connect;

public class TokenRequest
{
    [ModelBinder(Name = "grant_type")]
    public string GrantType { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    [ModelBinder(Name = "redirect_uri")]
    public Uri? RedirectUri { get; init; }
}
