namespace Deopeia.AdminPortal.Bff.Models.Me;

public sealed record TwoFactorAuthenticationResponse(string QrCodeImageUrl, string ManualEntryKey);
