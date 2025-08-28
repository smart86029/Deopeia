namespace Deopeia.Identity.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    string UserName,
    string Password,
    bool IsEnabled,
    List<string> RoleCodes
) : ICommand;
