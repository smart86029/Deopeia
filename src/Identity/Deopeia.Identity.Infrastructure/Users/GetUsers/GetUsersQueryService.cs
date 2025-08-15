using Deopeia.Identity.Application.Users.GetUsers;

namespace Deopeia.Identity.Infrastructure.Users.GetUsers;

public class GetUsersQueryService(NpgsqlConnection connection) : IGetUsersQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<UserDto>> GetAsync(GetUsersQuery query)
    {
        var builder = new SqlBuilder();
        if (!query.UserName.IsNullOrWhiteSpace())
        {
            builder.Where(
                "user_name LIKE @UserName",
                new { UserName = $"%{query.UserName.Trim()}%" }
            );
        }

        if (query.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { query.IsEnabled });
        }

        if (!query.RoleCode.IsNullOrWhiteSpace())
        {
            builder.Where(
                "id = ANY(SELECT user_id AS id FROM user_role WHERE role_code = @RoleCode)",
                new { query.RoleCode }
            );
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM \"user\" /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<UserDto>(query, count);

        var sql = builder.AddTemplate(
            """
SELECT
    id,
    user_name,
    is_enabled
FROM "user"
/**where**/
GROUP BY id
LIMIT @Limit
OFFSET @Offset
""",
            new { result.Limit, result.Offset }
        );
        var users = await _connection.QueryAsync<UserDto>(sql.RawSql, sql.Parameters);

        var sqlRole = """
SELECT
    user_id,
    role_code
FROM user_role
WHERE user_id = ANY(@UserIds)
""";

        var userRoles = await _connection.QueryAsync<(Guid UserId, string RoleCode)>(
            sqlRole,
            new { UserIds = users.Select(x => x.Id).ToList() }
        );
        var lookup = userRoles.ToLookup(x => x.UserId, x => x.RoleCode);
        foreach (var user in users)
        {
            user.RoleCodes = lookup[user.Id].ToList();
        }

        result.Items = users.ToList();

        return result;
    }
}
