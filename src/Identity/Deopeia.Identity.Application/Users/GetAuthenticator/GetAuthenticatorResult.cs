namespace Deopeia.Identity.Application.Users.GetAuthenticator;

public sealed record GetAuthenticatorResult
{
    public bool IsEnabled { get; init; }

    public string? QrCodeImageUrl { get; init; }

    public string? ManualEntryKey { get; init; }
}
