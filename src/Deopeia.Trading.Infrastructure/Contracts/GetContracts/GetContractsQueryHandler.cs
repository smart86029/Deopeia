using Deopeia.Trading.Application.Contracts.GetContracts;

namespace Deopeia.Trading.Infrastructure.Contracts.GetContracts;

internal class GetContractsQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetContractsQuery, PageResult<ContractDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<PageResult<ContractDto>> Handle(
        GetContractsQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        if (request.UnderlyingType.HasValue)
        {
            builder.Where("a.underlying_type = @UnderlyingType", new { request.UnderlyingType });
        }

        if (request.TraderId.HasValue)
        {
            builder.InnerJoin(
                "trader_favorite AS b ON b.trader_id = @TraderId AND a.symbol = b.symbol",
                new { request.TraderId }
            );
            builder.OrderBy("b.sort_order");
        }

        builder.OrderBy("a.symbol");

        var sqlCount = builder.AddTemplate(
            "SELECT COUNT(*) FROM contract AS a /**innerjoin**/ /**where**/"
        );
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<ContractDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a.*,
    COALESCE(b.name, c.name) AS name,
    COALESCE(h.name, i.name) AS currency
FROM (
    SELECT
        a.symbol,
        a.currency_code,
        a.underlying_type
    FROM contract AS a
    /**innerjoin**/
    /**where**/
    /**orderby**/
    LIMIT @Limit
    OFFSET @Offset
) AS a
LEFT JOIN contract_locale AS b
    ON a.symbol = b.symbol AND b.culture = @CurrentCulture
INNER JOIN contract_locale AS c
    ON a.symbol = c.symbol AND c.culture = @DefaultThreadCurrentCulture
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
        var contracts = await _connection.QueryAsync<ContractDto>(sql.RawSql, sql.Parameters);
        result.Items = contracts.ToList();

        return result;
    }
}
