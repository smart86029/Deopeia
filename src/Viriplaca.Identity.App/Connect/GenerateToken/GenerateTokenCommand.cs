namespace Viriplaca.Identity.App.Connect.GenerateToken;

public record GenerateTokenCommand(
    string ClientId,
    Uri? RedirectUri,
    string GrantType,
    string Code,
    string CodeVerifier)
    : IRequest<TokenResult>
{
}
