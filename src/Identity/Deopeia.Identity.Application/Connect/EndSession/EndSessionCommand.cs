namespace Deopeia.Identity.Application.Connect.EndSession;

public record EndSessionCommand(
    string IdTokenHint,
    string? LogoutHint,
    string? ClientId,
    Uri? PostLogoutRedirectUri,
    string? State,
    string? UiLocales
) : ICommand<EndSessionResult> { }
