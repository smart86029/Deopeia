namespace Deopeia.Identity.Application.Authentication.SignIn;

public class SignInResult
{
    public Guid? UserId { get; set; }

    public bool IsTwoFactorEnabled { get; set; }
}
