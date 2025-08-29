namespace Deopeia.Identity.Application.Permissions.GetPermissionOptions;

internal sealed class GetPermissionOptionsQueryHandler(
    IGetPermissionOptionsQueryService queryService
) : IQueryHandler<GetPermissionOptionsQuery, IReadOnlyList<OptionResult<string>>>
{
    private readonly IGetPermissionOptionsQueryService _queryService = queryService;

    public async ValueTask<IReadOnlyList<OptionResult<string>>> Handle(
        GetPermissionOptionsQuery query,
        CancellationToken cancellationToken
    ) => await _queryService.ListAsync();
}
