namespace Deopeia.Identity.Application.Users;

public interface IAuthenticatorService
{
    SetupCode GenerateSetupCode(string secretKey, string title);

    bool ValidateVerificationCode(string secretKey, string verificationCode);
}
