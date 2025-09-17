using Deopeia.Identity.Application.Roles.GetRoles;

namespace Deopeia.Identity.Infrastructure.Roles.GetRoles;

public class GetRolesQueryService(NpgsqlConnection connection) : IGetRolesQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PagedResult<RoleDto>> GetAsync(GetRolesQuery query)
    {
        var builder = new SqlBuilder();
        if (query.IsEnabled.HasValue)
        {
            builder.Where("a.is_enabled = @IsEnabled", new { query.IsEnabled });
        }

        var counterSql = "SELECT COUNT(*) FROM role AS a /**where**/";
        var selectorSql = """
SELECT
    a.code,
    COALESCE(b.name, c.name, a.code) AS name,
    COALESCE(b.description, c.description) AS description,
    a.is_enabled
FROM role AS a
LEFT JOIN role_localization AS b ON a.code = b.role_code AND b.culture = @CurrentCulture
LEFT JOIN role_localization AS c ON a.code = c.role_code AND c.culture = @DefaultThreadCurrentCulture
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
