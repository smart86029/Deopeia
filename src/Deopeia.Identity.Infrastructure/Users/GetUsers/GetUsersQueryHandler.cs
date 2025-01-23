using Deopeia.Identity.Application.Users.GetUsers;

namespace Deopeia.Identity.Infrastructure.Users.GetUsers;

public class GetUsersQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetUsersQuery, PageResult<UserDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<UserDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (!request.UserName.IsNullOrWhiteSpace())
        {
            builder.Where(
                "user_name LIKE @UserName",
                new { UserName = $"%{request.UserName.Trim()}%" }
            );
        }

        if (request.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        if (!request.RoleCode.IsNullOrWhiteSpace())
        {
            builder.Where(
                "id = ANY(SELECT user_id AS id FROM user_role WHERE role_code = @RoleCode)",
                new { request.RoleCode }
            );
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM \"user\" /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<UserDto>(request, count);

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
