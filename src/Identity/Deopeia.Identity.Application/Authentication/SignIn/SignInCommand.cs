namespace Deopeia.Identity.Application.Authentication.SignIn;

public sealed record SignInCommand(string UserName, string Password, string Code)
    : ICommand<SignInResult>;
