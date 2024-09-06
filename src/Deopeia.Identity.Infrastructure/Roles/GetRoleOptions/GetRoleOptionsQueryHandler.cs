using Deopeia.Identity.Application.Roles.GetRoleOptions;

namespace Deopeia.Identity.Infrastructure.Roles.GetRoleOptions;

internal class GetRoleOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetRoleOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetRoleOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    COALESCE(b.name, c.name) AS name,
    a.is_enabled
FROM role AS a
LEFT JOIN role_locale AS b ON a.id = b.role_id AND b.culture = @CurrentCulture
INNER JOIN role_locale AS c ON a.id = c.role_id AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<Guid>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
