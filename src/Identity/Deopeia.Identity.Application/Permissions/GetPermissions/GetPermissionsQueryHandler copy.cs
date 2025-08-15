namespace Deopeia.Identity.Application.Permissions.GetPermissions;

public class GetPermissionsQueryHandler(IGetPermissionsQueryService queryService)
    : IQueryHandler<GetPermissionsQuery, PageResult<PermissionDto>>
{
    private readonly IGetPermissionsQueryService _queryService = queryService;

    public async ValueTask<PageResult<PermissionDto>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken
    ) => await _queryService.GetAsync(request);
}
