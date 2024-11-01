using Deopeia.Quote.Application.Candles.GetHistoricalData;

namespace Deopeia.Quote.Infrastructure.Candles.GetHistoricalData;

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
        if (Guid.TryParse(request.IdOrSymbol, out var instrumentId))
        {
            builder.Where("instrument_id = @InstrumentId", new { InstrumentId = instrumentId });
        }
        else
        {
            builder.Where("symbol = @Symbol", new { Symbol = request.IdOrSymbol });
        }

        var sql = builder.AddTemplate(
            """
SELECT
    timestamp AS date,
    open,
    high,
    low,
    close,
    volume
FROM candle
/**where**/
ORDER BY timestamp
LIMIT 1
"""
        );

        var quotes = await _connection.QueryAsync<CandleDto>(sql.RawSql, sql.Parameters);
        var result = new GetHistoricalDataViewModel { Quotes = quotes.ToList() };

        return result;
    }
}
