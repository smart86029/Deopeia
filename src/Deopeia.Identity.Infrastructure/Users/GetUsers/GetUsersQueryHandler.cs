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
        if (request.IsEnabled.HasValue)
        {
            builder.Where("is_enabled = @IsEnabled", new { request.IsEnabled });
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
LIMIT @Limit
OFFSET @Offset
""",
            new { result.Limit, result.Offset, }
        );
        var users = await _connection.QueryAsync<UserDto>(sql.RawSql, sql.Parameters);
        result.Items = users.ToList();

        return result;
    }
}
