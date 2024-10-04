using Deopeia.Quote.Application.ContractSpecifications.GetContractSpecifications;

namespace Deopeia.Quote.Infrastructure.ContractSpecifications.GetContractSpecifications;

internal class GetContractSpecificationsQueryHandler(
    NpgsqlConnection connection,
    IStringLocalizer localizer
) : IRequestHandler<GetContractSpecificationsQuery, PageResult<ContractSpecificationDto>>
{
    private readonly NpgsqlConnection _connection = connection;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<PageResult<ContractSpecificationDto>> Handle(
        GetContractSpecificationsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("type = @Futures", new { InstrumentType.Futures });

        if (!request.ExchangeId.IsNullOrWhiteSpace())
        {
            builder.Where("exchange_id = @ExchangeId", new { request.ExchangeId });
        }

        if (request.AssetId.HasValue)
        {
            builder.Where("underlying_asset_id = @AssetId", new { request.AssetId });
        }

        var sqlCount = builder.AddTemplate(
            "SELECT COUNT(*) FROM contract_specification /**where**/"
        );
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<ContractSpecificationDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.*,
    COALESCE(b.name, c.name) AS name,
    COALESCE(d.name, e.name) AS exchange,
    COALESCE(f.name, g.name) AS underlying_asset,
    COALESCE(h.name, i.name) AS currency
FROM (
    SELECT
        id,
        exchange_id,
        symbol,
        currency_code,
        underlying_asset_id
    FROM contract_specification
    /**where**/
    ORDER BY exchange_id, symbol
    LIMIT @Limit
    OFFSET @Offset
) AS a
LEFT JOIN contract_specification_locale AS b
    ON a.id = b.contract_specification_id AND b.culture = @CurrentCulture
INNER JOIN contract_specification_locale AS c
    ON a.id = c.contract_specification_id AND c.culture = @DefaultThreadCurrentCulture
LEFT JOIN exchange_locale AS d
    ON a.exchange_id = d.exchange_id AND d.culture = @CurrentCulture
INNER JOIN exchange_locale AS e
    ON a.exchange_id = e.exchange_id AND e.culture = @DefaultThreadCurrentCulture
LEFT JOIN asset_locale AS f
    ON a.underlying_asset_id = f.asset_id AND f.culture = @CurrentCulture
INNER JOIN asset_locale AS g
    ON a.underlying_asset_id = g.asset_id AND g.culture = @DefaultThreadCurrentCulture
LEFT JOIN currency_locale AS h
    ON a.currency_code = h.currency_code AND h.culture = @CurrentCulture
INNER JOIN currency_locale AS i
    ON a.currency_code = i.currency_code AND i.culture = @DefaultThreadCurrentCulture
""",
            new
            {
                CultureInfo.CurrentCulture,
                CultureInfo.DefaultThreadCurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var futuresContracts = await _connection.QueryAsync<ContractSpecificationDto>(
            sql.RawSql,
            sql.Parameters
        );
        result.Items = futuresContracts.ToList();

        return result;
    }
}
