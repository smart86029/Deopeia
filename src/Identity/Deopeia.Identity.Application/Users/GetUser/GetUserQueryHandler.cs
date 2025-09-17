namespace Deopeia.Identity.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IGetUserQueryService queryService)
    : IQueryHandler<GetUserQuery, GetUserResult>
{
    private readonly IGetUserQueryService _queryService = queryService;

    public async ValueTask<GetUserResult> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken
    ) => await _queryService.GetAsync(query);
}
