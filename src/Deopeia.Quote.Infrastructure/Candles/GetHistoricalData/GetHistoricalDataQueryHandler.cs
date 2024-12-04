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
        var sql = """
SELECT
    timestamp AS date,
    open,
    high,
    low,
    close,
    volume
FROM candle
WHERE symbol = @Symbol
ORDER BY timestamp
LIMIT 100
""";

        var quotes = await _connection.QueryAsync<CandleDto>(sql, request);
        var result = new GetHistoricalDataViewModel { Quotes = quotes.ToList() };

        return result;
    }
}
