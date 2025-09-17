namespace Deopeia.Identity.Application.Permissions.GetPermission;

public interface IGetPermissionService
{
    Task<GetPermissionResult> GetPermissionAsync(GetPermissionQuery query);
}
