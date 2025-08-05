namespace Deopeia.Identity.Application.Authentication.SignIn;

public record SignInCommand(string UserName, string Password, string Code) : ICommand<SignInResult>;
