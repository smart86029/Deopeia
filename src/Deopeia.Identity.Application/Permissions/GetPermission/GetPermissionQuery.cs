namespace Deopeia.Identity.Application.Permissions.GetPermission;

public record GetPermissionQuery(string Code) : IRequest<GetPermissionViewModel> { }
