using Deopeia.Identity.Application.Permissions.GetPermissionOptions;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermissionOptions;

internal class GetPermissionOptionsQueryService(NpgsqlConnection connection)
    : IGetPermissionOptionsQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<IReadOnlyList<OptionResult<string>>> ListAsync()
    {
        var sql = """
SELECT
    COALESCE(b.name, c.name, a.code) AS name,
    a.code AS value,
    a.is_enabled
FROM permission AS a
LEFT JOIN permission_localization AS b ON a.code = b.permission_code AND b.culture = @CurrentCulture
LEFT JOIN permission_localization AS c ON a.code = c.permission_code AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
