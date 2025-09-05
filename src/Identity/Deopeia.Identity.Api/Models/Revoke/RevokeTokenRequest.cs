using Deopeia.Identity.Application.Revoke.RevokeToken;

namespace Deopeia.Identity.Api.Models.Revoke;

public class RevokeTokenRequest
{
    [ModelBinder(Name = "token")]
    public string Token { get; init; } = string.Empty;

    [ModelBinder(Name = "token_type_hint")]
    public string TokenTypeHint { get; init; } = string.Empty;

    public RevokeTokenCommand ToCommand()
    {
        return new RevokeTokenCommand(Token, TokenTypeHint);
    }
}
