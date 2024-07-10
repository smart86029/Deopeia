using Deopeia.Quote.Application.Ohlcvs.GetHistoricalData;

namespace Deopeia.Quote.Infrastructure.Ohlcvs.GetHistoricalData;

internal class GetHistoricalDataQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetHistoricalDataQuery, GetHistoricalDataViewModel>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<GetHistoricalDataViewModel> Handle(
        GetHistoricalDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("\"Symbol\" = @Symbol", new { request.Symbol });
        var sql = builder.AddTemplate(
            """
SELECT
    "Symbol",
    "RecordedAt" AS "Date",
    "Open",
    "High",
    "Low",
    "Close",
    "Volume"
FROM "Quote"."Ohlcv"
/**where**/
ORDER BY "RecordedAt"
"""
        );
        var quotes = await _connection.QueryAsync<OhlcvDto>(sql.RawSql, sql.Parameters);
        var result = new GetHistoricalDataViewModel { Quotes = quotes.ToList() };

        return result;
    }
}
