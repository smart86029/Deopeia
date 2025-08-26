namespace Deopeia.Identity.Application.Roles.GetRoleOptions;

public interface IGetRoleOptionsQueryService
{
    Task<IReadOnlyList<OptionResult<string>>> ListAsync(CancellationToken cancellationToken);
}
