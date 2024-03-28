namespace Viriplaca.Identity.App.Authentication.SignIn;

public record SignInCommand(string UserName, string Password)
    : IRequest<AuthToken>
{
}
