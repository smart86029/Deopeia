namespace Deopeia.Identity.Application.Permissions.DeletePermission;

public sealed record DeletePermissionCommand(string Code) : ICommand;
