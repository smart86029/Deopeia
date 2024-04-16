using Viriplaca.Identity.App.Connect.GenerateToken;

namespace Viriplaca.Identity.Api.Models.Connect;

public class GenerateTokenRequest
{
    [ModelBinder(Name = "client_id")]
    public string ClientId { get; init; } = string.Empty;

    [ModelBinder(Name = "grant_type")]
    public string GrantType { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    [ModelBinder(Name = "code_verifier")]
    public string CodeVerifier { get; init; } = string.Empty;

    [ModelBinder(Name = "redirect_uri")]
    public Uri? RedirectUri { get; init; }

    public GenerateTokenCommand ToCommand()
    {
        return new GenerateTokenCommand(
            ClientId,
            RedirectUri,
            GrantType,
            Code,
            CodeVerifier);
    }
}
