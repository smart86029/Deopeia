namespace Deopeia.Identity.Application.Roles.GetRole;

internal sealed class GetRoleQueryHandler(IGetRoleQueryService queryService)
    : IQueryHandler<GetRoleQuery, GetRoleResult>
{
    private readonly IGetRoleQueryService _queryService = queryService;

    public async ValueTask<GetRoleResult> Handle(
        GetRoleQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
