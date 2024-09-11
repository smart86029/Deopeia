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
    a.id,
    COALESCE(b.name, c.name) AS name,
    a.is_enabled
FROM role AS a
LEFT JOIN role_locale AS b ON a.id = b.role_id AND b.culture = @CurrentCulture
INNER JOIN role_locale AS c ON a.id = c.role_id AND c.culture = @DefaultThreadCurrentCulture
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