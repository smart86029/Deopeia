using Deopeia.Identity.Application.Permissions.GetPermissions;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermissions;

public class GetPermissionsQueryService(NpgsqlConnection connection) : IGetPermissionsQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PagedResult<PermissionDto>> GetAsync(GetPermissionsQuery query)
    {
        var builder = new SqlBuilder();
        if (!query.Code.IsNullOrWhiteSpace())
        {
            builder.Where("code LIKE @Code", new { Code = $"%{query.Code.Trim()}%" });
        }

        if (query.IsEnabled.HasValue)
        {
            builder.Where("a.is_enabled = @IsEnabled", new { query.IsEnabled });
        }

        var counterSql = "SELECT COUNT(*) FROM permission AS a /**where**/";
        var selectorSql = """
SELECT
    a.code,
    COALESCE(b.name, c.name, a.code) AS name,
    COALESCE(b.description, c.description) AS description,
    a.is_enabled
FROM permission AS a
LEFT JOIN permission_localization AS b ON a.code = b.permission_code AND b.culture = @CurrentCulture
LEFT JOIN permission_localization AS c ON a.code = c.permission_code AND c.culture = @DefaultThreadCurrentCulture
/**where**/
ORDER BY code
/**pagination**/
""";
        builder.AddParameters(
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );
        return await _connection.QueryPagedResultAsync(builder, counterSql, selectorSql, query);
    }
}
