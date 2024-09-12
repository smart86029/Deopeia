using Dapper;
using Deopeia.Identity.Application.Users.GetUsers;
using Deopeia.Identity.Domain.Users;

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
        if (request.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        if (request.RoleId.HasValue)
        {
            builder.Where(
                "id = ANY(SELECT user_id AS id FROM user_role WHERE role_id = @RoleId)",
                new { request.RoleId }
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
            new { result.Limit, result.Offset, }
        );
        var users = await _connection.QueryAsync<UserDto>(sql.RawSql, sql.Parameters);
        result.Items = users.ToList();

        var sqlRole = """
SELECT
    user_id,
    role_id
FROM user_role
WHERE user_id = ANY(@UserIds)
""";

        var userRoles = await _connection.QueryAsync<(Guid UserId, Guid RoleId)>(
            sqlRole,
            new { UserIds = users.Select(x => x.Id).ToList() }
        );
        var lookup = userRoles.ToLookup(x => x.UserId, x => x.RoleId);
        foreach (var user in users)
        {
            user.RoleIds = lookup[user.Id].ToList();
        }

        result.Items = users.ToList();

        return result;
    }
}
