namespace Deopeia.Identity.Application.Users.GetAuthenticator;

public class GetAuthenticatorResult
{
    public bool IsEnabled { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string ManualEntryKey { get; set; } = string.Empty;
}
