namespace Deopeia.Identity.Application.Users.EnableAuthenticator;

public record EnableAuthenticatorCommand(Guid Id, string VerificationCode) : ICommand;
