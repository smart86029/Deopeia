namespace Deopeia.Identity.Application.Permissions.GetPermission;

public record GetPermissionQuery(string Code) : IQuery<GetPermissionViewModel>;
