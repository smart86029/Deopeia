using Deopeia.Quote.Application.Stocks.GetStocks;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Stocks.GetStocks;

internal class GetStocksQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetStocksQuery, PageResult<StockDto>>
{
    public async Task<PageResult<StockDto>> Handle(
        GetStocksQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("a.\"Type\" = @Stock", new { MarketType.Stock });

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM \"Instrument\" AS a /**where**/");
        var count = await connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<StockDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a."Symbol",
    b."Name"
FROM "Instrument" AS a
INNER JOIN "InstrumentLocale" AS b ON a."Id" = b."InstrumentId" AND b."Culture" = @Culture
/**where**/
LIMIT @Limit
OFFSET @Offset
""",
            new
            {
                Culture = CultureInfo.CurrentCulture,
                result.Limit,
                result.Offset,
            }
        );
        var stocks = await connection.QueryAsync<StockDto>(sql.RawSql, sql.Parameters);
        result.Items = stocks.ToList();

        return result;
    }
}
