namespace Deopeia.Identity.Application.Permissions.GetPermissionOptions;

public interface IGetPermissionOptionsQueryService
{
    Task<ICollection<OptionResult<string>>> ListAsync();
}
