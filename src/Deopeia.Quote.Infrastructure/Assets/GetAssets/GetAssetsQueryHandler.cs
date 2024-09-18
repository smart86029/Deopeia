using Deopeia.Quote.Application.Assets.GetAssets;

namespace Deopeia.Quote.Infrastructure.Assets.GetAssets;

public class GetAssetsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAssetsQuery, PageResult<AssetDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<AssetDto>> Handle(
        GetAssetsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM asset AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<AssetDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.id,
    a.code,
    COALESCE(b.name, c.name) AS name,
    COALESCE(b.description, c.description) AS description
FROM asset AS a
LEFT JOIN asset_locale AS b ON a.id = b.asset_id AND b.culture = @CurrentCulture
INNER JOIN asset_locale AS c ON a.id = c.asset_id AND c.culture = @DefaultThreadCurrentCulture
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
        var assets = await _connection.QueryAsync<AssetDto>(sql.RawSql, sql.Parameters);
        result.Items = assets.ToList();

        return result;
    }
}
