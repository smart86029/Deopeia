namespace Deopeia.Identity.Application.Roles.GetRoles;

internal sealed class GetRolesQueryHandler(IGetRolesQueryService queryService)
    : IQueryHandler<GetRolesQuery, PagedResult<RoleDto>>
{
    private readonly IGetRolesQueryService _queryService = queryService;

    public async ValueTask<PagedResult<RoleDto>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(request);
    }
}
