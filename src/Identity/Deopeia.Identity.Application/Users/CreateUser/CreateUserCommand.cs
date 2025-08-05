namespace Deopeia.Identity.Application.Users.CreateUser;

public record CreateUserCommand(string UserName, string Password, bool IsEnabled) : ICommand;
