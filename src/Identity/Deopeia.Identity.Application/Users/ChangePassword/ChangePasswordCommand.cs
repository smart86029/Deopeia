namespace Deopeia.Identity.Application.Users.ChangePassword;

public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword)
    : ICommand;
