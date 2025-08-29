namespace Deopeia.Identity.Application.Permissions.GetPermission;

public sealed record GetPermissionQuery(string Code) : IQuery<GetPermissionViewModel>;
