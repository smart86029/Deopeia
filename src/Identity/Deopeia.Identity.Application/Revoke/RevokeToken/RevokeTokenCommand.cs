namespace Deopeia.Identity.Application.Revoke.RevokeToken;

public record RevokeTokenCommand(string Token, string TokenTypeHint) : ICommand<RevokeTokenResult>;
