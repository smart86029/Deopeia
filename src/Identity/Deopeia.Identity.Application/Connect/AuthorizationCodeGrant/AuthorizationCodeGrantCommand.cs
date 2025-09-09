namespace Deopeia.Identity.Application.Connect.AuthorizationCodeGrant;

public sealed record AuthorizationCodeGrantCommand(
    string ClientId,
    Uri? RedirectUri,
    string GrantType,
    string Code,
    string CodeVerifier
) : GrantCommand { }
