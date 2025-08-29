namespace Deopeia.Identity.Application.Roles.GetRoleOptions;

internal sealed class GetRoleOptionsQueryHandler(IGetRoleOptionsQueryService queryService)
    : IQueryHandler<GetRoleOptionsQuery, IReadOnlyList<OptionResult<string>>>
{
    private readonly IGetRoleOptionsQueryService _queryService = queryService;

    public async ValueTask<IReadOnlyList<OptionResult<string>>> Handle(
        GetRoleOptionsQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.ListAsync(cancellationToken);
    }
}
