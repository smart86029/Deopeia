namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public interface IGetPermissionsQueryService
{
    Task<PagedResult<PermissionDto>> GetAsync(GetPermissionsQuery query);
}
