using Deopeia.Quote.Application.Assets.GetAssetOptions;

namespace Deopeia.Quote.Infrastructure.Assets.GetAssetOptions;

internal class GetAssetOptionsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAssetOptionsQuery, ICollection<OptionResult<Guid>>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<ICollection<OptionResult<Guid>>> Handle(
        GetAssetOptionsQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    a.id AS value,
    COALESCE(b.name, c.name) AS name
FROM asset AS a
LEFT JOIN asset_locale AS b ON a.id = b.asset_id AND b.culture = @CurrentCulture
INNER JOIN asset_locale AS c ON a.id = c.asset_id AND c.culture = @DefaultThreadCurrentCulture
""";
        var optoins = await _connection.QueryAsync<OptionResult<Guid>>(
            sql,
            new { CultureInfo.CurrentCulture, CultureInfo.DefaultThreadCurrentCulture }
        );

        return optoins.ToList();
    }
}
