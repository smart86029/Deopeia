namespace Deopeia.Identity.Application.Roles.GetRole;

internal sealed class GetRoleQueryHandler(IGetRoleQueryService queryService)
    : IQueryHandler<GetRoleQuery, GetRoleViewModel>
{
    private readonly IGetRoleQueryService _queryService = queryService;

    public async ValueTask<GetRoleViewModel> Handle(
        GetRoleQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
