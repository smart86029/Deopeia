using Deopeia.Common.Localization;
using Deopeia.Quote.Application.Stocks.GetStocks;
using Deopeia.Quote.Domain.Companies;
using Deopeia.Quote.Domain.Instruments;

namespace Deopeia.Quote.Infrastructure.Stocks.GetStocks;

internal class GetStocksQueryHandler(NpgsqlConnection connection, IStringLocalizer localizer)
    : IRequestHandler<GetStocksQuery, PageResult<StockDto>>
{
    private readonly NpgsqlConnection _connection = connection;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<PageResult<StockDto>> Handle(
        GetStocksQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("a.\"Type\" = @Stock", new { MarketType.Stock });

        var sqlCount = builder.AddTemplate("SELECT COUNT(*) FROM \"Instrument\" AS a /**where**/");
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<StockDto>(request, count);

        var sql = builder.AddTemplate(
            """
SELECT
    a."Symbol",
    b."Name",
    c."SubIndustry"
FROM "Instrument" AS a
INNER JOIN "InstrumentLocale" AS b ON a."Id" = b."InstrumentId" AND b."Culture" = @Culture
/**where**/
ORDER BY a."Symbol"
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
        var stocks = await _connection.QueryAsync<StockDto>(sql.RawSql, sql.Parameters);
        foreach (var stock in stocks)
        {
            stock.Industry = _localizer.GetEnumString(stock.SubIndustry.ToIndustry());
        }

        result.Items = stocks.ToList();

        return result;
    }
}
