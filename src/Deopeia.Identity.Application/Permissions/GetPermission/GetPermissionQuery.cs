namespace Deopeia.Identity.Application.Permissions.GetPermission;

public record GetPermissionQuery(Guid Id) : IRequest<GetPermissionViewModel> { }
