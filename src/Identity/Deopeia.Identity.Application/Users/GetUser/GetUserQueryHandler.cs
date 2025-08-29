namespace Deopeia.Identity.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IGetUserQueryService queryService)
    : IQueryHandler<GetUserQuery, GetUserViewModel>
{
    private readonly IGetUserQueryService _queryService = queryService;

    public async ValueTask<GetUserViewModel> Handle(
        GetUserQuery query,
        CancellationToken cancellationToken
    ) => await _queryService.GetAsync(query);
}
