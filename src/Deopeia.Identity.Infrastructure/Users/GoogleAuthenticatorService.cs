using Deopeia.Identity.Application.Users;
using Google.Authenticator;
using SetupCode = Deopeia.Identity.Application.Users.SetupCode;

namespace Deopeia.Identity.Infrastructure.Users;

internal class GoogleAuthenticatorService : IAuthenticatorService
{
    public SetupCode GenerateSetupCode(string secretKey, string title)
    {
        var setupCode = new TwoFactorAuthenticator().GenerateSetupCode(
            "Deopeia",
            title,
            secretKey,
            true
        );

        return new SetupCode(setupCode.QrCodeSetupImageUrl, setupCode.ManualEntryKey);
    }

    public bool ValidateVerificationCode(string secretKey, string verificationCode)
    {
        return new TwoFactorAuthenticator().ValidateTwoFactorPIN(secretKey, verificationCode, true);
    }
}
