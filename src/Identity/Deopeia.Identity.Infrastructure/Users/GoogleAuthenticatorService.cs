using Deopeia.Identity.Application.Users;
using Google.Authenticator;
using SetupCode = Deopeia.Identity.Application.Users.SetupCode;

namespace Deopeia.Identity.Infrastructure.Users;

internal sealed class GoogleAuthenticatorService : IAuthenticatorService
{
    private readonly TwoFactorAuthenticator _twoFactorAuthenticator = new();

    public SetupCode GenerateSetupCode(string secretKey, string title)
    {
        var setupCode = _twoFactorAuthenticator.GenerateSetupCode(
            "Deopeia",
            title,
            secretKey,
            true
        );
        return new SetupCode(setupCode.QrCodeSetupImageUrl, setupCode.ManualEntryKey);
    }

    public bool ValidateTwoFactorCode(string secretKey, string verificationCode)
    {
        return _twoFactorAuthenticator.ValidateTwoFactorPIN(secretKey, verificationCode, true);
    }
}
