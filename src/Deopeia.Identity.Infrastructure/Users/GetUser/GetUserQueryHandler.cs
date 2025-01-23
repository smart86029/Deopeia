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
WHERE id = @Id;

SELECT role_code
FROM user_role
WHERE user_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetUserViewModel>();
        result.RoleCodes = multiple.Read<string>().ToList();

        return result;
    }
}
