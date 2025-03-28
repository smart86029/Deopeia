using Deopeia.Identity.Application.Connect.EndSession;

namespace Deopeia.Identity.Api.Models.Connect;

public class EndSessionRequest
{
    [ModelBinder(Name = "id_token_hint")]
    public string IdTokenHint { get; init; } = string.Empty;

    [ModelBinder(Name = "logout_hint")]
    public string? LogoutHint { get; init; }

    [ModelBinder(Name = "client_id")]
    public string? ClientId { get; init; }

    [ModelBinder(Name = "post_logout_redirect_uri")]
    public Uri? PostLogoutRedirectUri { get; init; }

    public string? State { get; init; }

    [ModelBinder(Name = "ui_locales")]
    public string? UiLocales { get; init; }

    public EndSessionCommand ToCommand()
    {
        return new EndSessionCommand(
            IdTokenHint,
            LogoutHint,
            ClientId,
            PostLogoutRedirectUri,
            State,
            UiLocales
        );
    }
}
