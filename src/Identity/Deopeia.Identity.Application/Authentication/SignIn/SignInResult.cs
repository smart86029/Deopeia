namespace Deopeia.Identity.Application.Authentication.SignIn;

public sealed record SignInResult
{
    public Guid? UserId { get; init; }

    public bool IsTwoFactorEnabled { get; init; }
}
