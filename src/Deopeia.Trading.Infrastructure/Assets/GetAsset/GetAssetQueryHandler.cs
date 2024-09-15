using Deopeia.Trading.Application.Assets;
using Deopeia.Trading.Application.Assets.GetAsset;

namespace Deopeia.Trading.Infrastructure.Assets.GetAsset;

public class GetAssetQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetAssetQuery, GetAssetViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetAssetViewModel> Handle(
        GetAssetQuery request,
        CancellationToken cancellationToken
    )
    {
        var sql = """
SELECT
    id,
    code
FROM asset
WHERE id = @Id;

SELECT
    culture,
    name,
    description
FROM asset_locale
WHERE asset_id = @Id;
""";
        using var multiple = await _connection.QueryMultipleAsync(sql, request);
        var result = multiple.ReadFirst<GetAssetViewModel>();
        result.Locales = multiple.Read<AssetLocaleDto>().ToList();

        return result;
    }
}
