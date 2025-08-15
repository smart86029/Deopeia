using Deopeia.Identity.Application.Permissions.GetPermissions;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermissions;

public class GetPermissionsQueryService(NpgsqlConnection connection) : IGetPermissionsQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<PermissionDto>> GetAsync(GetPermissionsQuery request)
    {
        var builder = new SqlBuilder();
        if (!request.Code.IsNullOrWhiteSpace())
        {
            builder.Where("code LIKE @Code", new { Code = $"%{request.Code.Trim()}%" });
        }

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
    a.code,
    COALESCE(b.name, c.name) AS name,
    COALESCE(b.description, c.description) AS description,
    a.is_enabled
FROM permission AS a
LEFT JOIN permission_locale AS b ON a.code = b.permission_code AND b.culture = @CurrentCulture
INNER JOIN permission_locale AS c ON a.code = c.permission_code AND c.culture = @DefaultThreadCurrentCulture
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
