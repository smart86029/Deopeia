namespace Deopeia.Identity.Application.Users.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string UserName,
    string? Password,
    bool IsEnabled,
    ICollection<string> RoleCodes
) : ICommand;
