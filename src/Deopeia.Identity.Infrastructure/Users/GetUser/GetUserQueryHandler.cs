using Deopeia.Identity.Application.Users.GetUser;

namespace Deopeia.Identity.Infrastructure.Users.GetUser;

public class GetUserQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetUserQuery, GetUserViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetUserViewModel> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    user_name,
    is_enabled
FROM "user"
WHERE id = @Id
""";
        var result = await _connection.QuerySingleAsync<GetUserViewModel>(sql, request);

        return result;
    }
}
