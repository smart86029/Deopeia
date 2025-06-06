using Deopeia.Identity.Application.Permissions.GetPermissionOptions;

namespace Deopeia.Identity.Infrastructure.Permissions.GetPermissionOptions;

internal class GetPermissionOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetPermissionOptionsQuery, ICollection<OptionResult<string>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<string>>> Handle(
        GetPermissionOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.code AS value,
    COALESCE(b.name, c.name) AS name,
    a.is_enabled
FROM role AS a
LEFT JOIN role_locale AS b ON a.code = b.role_code AND b.culture = @CurrentCulture
INNER JOIN role_locale AS c ON a.code = c.role_code AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<string>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
