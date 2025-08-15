namespace Deopeia.Identity.Application.Users.GetUser;

public sealed class GetUserQueryHandler(IGetUserQueryService queryService)
    : IQueryHandler<GetUserQuery, GetUserViewModel>
{
    private readonly IGetUserQueryService _queryService = queryService;

    public async ValueTask<GetUserViewModel> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken
    )
    {
        return await _queryService.GetAsync(query);
    }
}
