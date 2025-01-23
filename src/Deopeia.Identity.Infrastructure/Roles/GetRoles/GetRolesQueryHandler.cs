using Deopeia.Identity.Application.Roles.GetRoles;

namespace Deopeia.Identity.Infrastructure.Roles.GetRoles;

public class GetRolesQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetRolesQuery, PageResult<RoleDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<RoleDto>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.IsEnabled.HasValue)
        {
            builder.Where("a.is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM role AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<RoleDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.code,
    COALESCE(b.name, c.name) AS name,
    COALESCE(b.description, c.description) AS description,
    a.is_enabled
FROM role AS a
LEFT JOIN role_locale AS b ON a.code = b.role_code AND b.culture = @CurrentCulture
INNER JOIN role_locale AS c ON a.code = c.role_code AND c.culture = @DefaultThreadCurrentCulture
/**where**/
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var roles = await _connection.QueryAsync<RoleDto>(sql.RawSql, sql.Parameters);
        result.Items = roles.ToList();

        return result;
    }
}
