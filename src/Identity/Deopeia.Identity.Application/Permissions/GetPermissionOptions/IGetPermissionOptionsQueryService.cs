namespace Deopeia.Identity.Application.Permissions.GetPermissionOptions;

public interface IGetPermissionOptionsQueryService
{
    Task<IReadOnlyList<OptionResult<string>>> ListAsync();
}
