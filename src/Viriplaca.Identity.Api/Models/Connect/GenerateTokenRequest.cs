using Viriplaca.Identity.App.Connect;
using Viriplaca.Identity.App.Connect.AuthorizationCodeGrant;
using Viriplaca.Identity.App.Connect.RefreshTokenGrant;
using Viriplaca.Identity.Domain.Grants;

namespace Viriplaca.Identity.Api.Models.Connect;

public class GenerateTokenRequest
{
    [ModelBinder(Name = "client_id")]
    public string ClientId { get; init; } = string.Empty;

    [ModelBinder(Name = "grant_type")]
    public string GrantType { get; init; } = string.Empty;

    [ModelBinder(Name = "code")]
    public string Code { get; init; } = string.Empty;

    [ModelBinder(Name = "code_verifier")]
    public string CodeVerifier { get; init; } = string.Empty;

    [ModelBinder(Name = "refresh_token")]
    public string RefreshToken { get; init; } = string.Empty;

    [ModelBinder(Name = "redirect_uri")]
    public Uri? RedirectUri { get; init; }

    public GrantCommand ToCommand()
    {
        if (GrantType == GrantTypes.RefreshToken.ToString().ToSnakeCaseLower())
        {
            return new RefreshTokenGrantCommand(ClientId, RefreshToken);
        }

        return new AuthorizationCodeGrantCommand(
            ClientId,
            RedirectUri,
            GrantType,
            Code,
            CodeVerifier
        );
    }
}
