namespace Deopeia.Identity.Application.Users;

public sealed record SetupCode(string QrCodeImageUrl, string ManualEntryKey);
