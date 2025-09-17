using Deopeia.Identity.Application.Roles.GetRoleOptions;

namespace Deopeia.Identity.Infrastructure.Roles.GetRoleOptions;

internal class GetRoleOptionsQueryService(NpgsqlConnection connection) : IGetRoleOptionsQueryService
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<IReadOnlyList<OptionResult<string>>> ListAsync(
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    COALESCE(b.name, c.name, a.code) AS name,
    a.code AS value,
    a.is_enabled
FROM role AS a
LEFT JOIN role_localization AS b ON a.code = b.role_code AND b.culture = @CurrentCulture
LEFT JOIN role_localization AS c ON a.code = c.role_code AND c.culture = @DefaultThreadCurrentCulture
""";
        var options = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return options.ToList();
    }
}
