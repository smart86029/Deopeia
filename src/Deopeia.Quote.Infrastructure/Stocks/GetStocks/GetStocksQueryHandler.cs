using Deopeia.Common.Localization;
using Deopeia.Quote.Application.Stocks.GetStocks;
using Deopeia.Quote.Domain.Companies;

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
        builder.InnerJoin("company AS b ON a.company_id = b.id");

        builder.Where("a.type = @Stock", new { MarketType.Stock });
        if (request.Industry.HasValue)
        {
            builder.Where("b.sub_industry / 100 = @Industry", new { request.Industry });
        }

        var sqlCount = builder.AddTemplate(
            "SELECT COUNT(*) FROM instrument AS a /**innerjoin**/ /**where**/"
        );
        var count = await _connection.ExecuteScalarAsync<int>(sqlCount.RawSql, sqlCount.Parameters);
        var result = new PageResult<StockDto>(request, count);

        builder.InnerJoin(
            "instrument_locale AS c ON a.id = c.instrument_id AND c.culture = @Culture",
            new { Culture = CultureInfo.CurrentCulture }
        );
        var sql = builder.AddTemplate(
            """
SELECT
    a.symbol,
    c.name,
    b.sub_industry
FROM instrument AS a
/**innerjoin**/
/**where**/
ORDER BY a.symbol
LIMIT @Limit
OFFSET @Offset
""",
            new { result.Limit, result.Offset, }
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
