namespace Deopeia.Identity.Application.Permissions.GetPermissionOptions;

public sealed class GetPermissionOptionsQueryHandler(IGetPermissionOptionsQueryService queryService)
    : IQueryHandler<GetPermissionOptionsQuery, IReadOnlyList<OptionResult<string>>>
{
    private readonly IGetPermissionOptionsQueryService _queryService = queryService;

    public async ValueTask<IReadOnlyList<OptionResult<string>>> Handle(
        GetPermissionOptionsQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.ListAsync();
    }
}
