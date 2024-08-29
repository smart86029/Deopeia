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
        builder.Where("symbol = @Symbol", new { request.Symbol });
        var sql = builder.AddTemplate(
            """
SELECT
    symbol,
    recorded_at AS date,
    open,
    high,
    low,
    close,
    volume
FROM ohlcv
/**where**/
ORDER BY recorded_at
"""
        );
        var quotes = await _connection.QueryAsync<OhlcvDto>(sql.RawSql, sql.Parameters);
        var result = new GetHistoricalDataViewModel { Quotes = quotes.ToList() };

        return result;
    }
}
