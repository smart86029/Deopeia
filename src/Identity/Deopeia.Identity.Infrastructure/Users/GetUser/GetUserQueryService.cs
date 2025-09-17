using Deopeia.Identity.Application.Users.GetUser;

namespace Deopeia.Identity.Infrastructure.Users.GetUser;

public class GetUserQueryService(NpgsqlConnection connection) : IGetUserQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetUserResult> GetAsync(GetUserQuery query)
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
        using var multiple = await _connection.QueryMultipleAsync(sql, query);
        var result = multiple.ReadFirst<GetUserResult>();
        result.RoleCodes = multiple.Read<string>().ToList();

        return result;
    }
}
