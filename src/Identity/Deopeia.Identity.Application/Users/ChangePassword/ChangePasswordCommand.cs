namespace Deopeia.Identity.Application.Users.ChangePassword;

public record ChangePasswordCommand(Guid Id, string CurrentPassword, string NewPassword) : ICommand;
