namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public interface IGetPermissionsQueryService
{
    Task<PageResult<PermissionDto>> GetAsync(GetPermissionsQuery query);
}
