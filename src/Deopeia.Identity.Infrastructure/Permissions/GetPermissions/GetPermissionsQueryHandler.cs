using Deopeia.Identity.Application.Permissions.GetPermissions;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermissions;

public class GetPermissionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetPermissionsQuery, PageResult<PermissionDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<PermissionDto>> Handle(
        GetPermissionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.IsEnabled.HasValue)
        {
            builder.Where("a.is_enabled = @IsEnabled", new { request.IsEnabled });
        }

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM permission AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<PermissionDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.code,
    COALESCE(b.name, c.name) AS name,
    a.is_enabled
FROM permission AS a
LEFT JOIN permission_locale AS b ON a.id = b.permission_id AND b.culture = @CurrentCulture
INNER JOIN permission_locale AS c ON a.id = c.permission_id AND c.culture = @DefaultThreadCurrentCulture
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
        var permissions = await _connection.QueryAsync<PermissionDto>(sql.RawSql, sql.Parameters);
        result.Items = permissions.ToList();

        return result;
    }
}