namespace Viriplaca.Identity.App.Connect.GenerateToken;

public record GenerateTokenCommand(
    string Code,
    ClaimsPrincipal Subject)
    : IRequest<TokenResult>
{
}
