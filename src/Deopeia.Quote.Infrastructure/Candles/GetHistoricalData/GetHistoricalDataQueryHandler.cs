using Deopeia.Quote.Application.Candles.GetHistoricalData;

namespace Deopeia.Quote.Infrastructure.Candles.GetHistoricalData;

internal class GetHistoricalDataQueryHandler(NpgsqlConnection connection)
    : IRequestHandler<GetHistoricalDataQuery, List<CandleDto>>
{
    private readonly NpgsqlConnection _connection = connection;

    public async Task<List<CandleDto>> Handle(
        GetHistoricalDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var builder = new SqlBuilder();
        builder.Where("symbol = @Symbol", new { request.Symbol });
        builder.Where("time_frame = @TimeFrame", new { request.TimeFrame });

        if (request.StartedAt.HasValue)
        {
            builder.Where("timestamp <= @StartedAt", new { request.StartedAt });
        }

        var sql = builder.AddTemplate(
            """
SELECT
    timestamp,
    open,
    high,
    low,
    close,
    volume
FROM candle
/**where**/
ORDER BY timestamp
LIMIT 100
"""
        );
        var candles = await _connection.QueryAsync<CandleDto>(sql.RawSql, sql.Parameters);

        return candles.ToList();
    }
}
