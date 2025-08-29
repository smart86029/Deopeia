namespace Deopeia.Identity.Application.Permissions.GetPermission;

public interface IGetPermissionService
{
    Task<GetPermissionViewModel> GetPermissionAsync(GetPermissionQuery query);
}
