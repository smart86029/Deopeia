namespace Viriplaca.Identity.App.Connect.RefreshTokenGrant;

public record RefreshTokenGrantCommand(
    string ClientId,
    string RefreshToken)
    : GrantCommand
{
}
