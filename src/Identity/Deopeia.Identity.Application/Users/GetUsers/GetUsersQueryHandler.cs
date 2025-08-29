namespace Deopeia.Identity.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler(IGetUsersQueryService queryService)
    : IQueryHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IGetUsersQueryService _queryService = queryService;

    public async ValueTask<PagedResult<UserDto>> Handle(
        GetUsersQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
