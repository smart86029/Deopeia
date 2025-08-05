namespace Deopeia.Identity.Application.Connect.RefreshTokenGrant;

public record RefreshTokenGrantCommand(string ClientId, string RefreshToken) : GrantCommand { }
