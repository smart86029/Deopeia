namespace Deopeia.Identity.Application.Users.GetUsers;

public sealed class GetUsersQueryHandler(IGetUsersQueryService queryService)
    : IQueryHandler<GetUsersQuery, PageResult<UserDto>>
{
    private readonly IGetUsersQueryService _queryService = queryService;

    public async ValueTask<PageResult<UserDto>> Handle(
        GetUsersQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
