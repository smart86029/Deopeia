namespace Deopeia.Identity.Application.Permissions.GetPermissions;

internal class GetPermissionsQueryHandler(IGetPermissionsQueryService queryService)
    : IQueryHandler<GetPermissionsQuery, PagedResult<PermissionDto>>
{
    private readonly IGetPermissionsQueryService _queryService = queryService;

    public async ValueTask<PagedResult<PermissionDto>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken
    ) => await _queryService.GetAsync(request);
}
