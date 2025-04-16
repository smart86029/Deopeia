namespace Deopeia.Finance.Bff.Models.Identity;

public class EnableAuthenticatorCommand
{
    public string VerificationCode { get; set; } = string.Empty;
}
